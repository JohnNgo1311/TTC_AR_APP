#define STANDARD_DETECTION
#define WECHAT_DETECTION
#undef STANDARD_DETECTION
//#undef WECHAT_DETECTION

using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.UnityUtils.Helper;
using OpenCVForUnity.Wechat_qrcodeModule;
using OpenCVForUnityExample;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

/// <summary>
/// QRCodeDetector Example
/// An example of detecting QRCode using the QRCodeDetector class.
/// https://github.com/opencv/opencv/blob/master/samples/cpp/qrcode.cpp
/// </summary>
[RequireComponent(typeof(MultiSource2MatHelper))]
public class NewQRCodeDetectorMulti : MonoBehaviour
{
    [Header("Output")]
    /// <summary>
    /// The RawImage for previewing the result.
    /// </summary>
    public RawImage resultPreview;

    [Space(10)]

    /// <summary>
    /// The length of the markers' side. Normally, unit is meters.
    /// </summary>
    public float markerLength = 0.1f;

    [Space(10)]
    public float deltaTime = 0;

    /// <summary>
    /// The enable low pass filter toggle.
    /// </summary>
    public Toggle enableLowPassFilterToggle;

    ///// <summary>
    ///// ARHelper
    ///// </summary>
    //public ARHelper arHelper;

    /// <summary>
    /// ARHelper
    /// </summary>
    public ARHelperMulti arHelper;
    public Manager manager;

    [Space(10)]

    /// <summary>
    /// The gray mat.
    /// </summary>
    Mat grayMat;

    /// <summary>
    /// The texture.
    /// </summary>
    Texture2D texture;

    /// <summary>
    /// The QRCode detector.
    /// </summary>
    QRCodeDetector detector;

    /// <summary>
    /// The WeChat QR Code Detector
    /// </summary>
    WeChatQRCode wechatDetector;

    /// <summary>
    /// The points.
    /// </summary>
    Mat points;

    /// <summary>
    /// The decoded info
    /// </summary>
    List<string> decodedInfo;

    /// <summary>
    /// The straight qrcode
    /// </summary>
    List<Mat> straightQrcode;

    /// <summary>
    /// The multi source to mat helper.
    /// </summary>
    MultiSource2MatHelper multiSource2MatHelper;

    /// <summary>
    /// The cameraparam matrix.
    /// </summary>
    Mat camMatrix;

    /// <summary>
    /// The distortion coeffs.
    /// </summary>
    MatOfDouble distCoeffs;

    /// <summary>
    /// The FPS monitor.
    /// </summary>
    FpsMonitor fpsMonitor;

    /// <summary>
    /// Last frame time
    /// </summary>
    private DateTime lastFrameTime;

    /// <summary>
    /// Detection result
    /// </summary>
    private bool result;

    /// <summary>
    /// List of detected point sets
    /// </summary>
    private List<Mat> pointSetList = new List<Mat>();

    public bool detectionAck { get; private set; } = true;
    public Coroutine coroutine { get; private set; }
    private bool isChangeOrientation = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartDetect());
    }

    private void Awake()
    {
        multiSource2MatHelper ??= gameObject.GetComponent<MultiSource2MatHelper>();
        StartCoroutine(InitCoroutine());
    }
    private IEnumerator InitCoroutine()
    {
        if (isChangeOrientation == false)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            yield return new WaitForSeconds(0.05f);
            isChangeOrientation = true;
        }
    }

    private IEnumerator StartDetect()
    {
        yield return new WaitUntil(() => isChangeOrientation == true);

        Utils.setDebugMode(true);
        multiSource2MatHelper.outputColorFormat = Source2MatHelperColorFormat.RGBA;

        InitQrDetector();
        InitWeChatDetector();
        //var shapes = arHelper.arGameObjectOrigin.GetComponentsInChildren<MeshRenderer>();
        //foreach (var shape in shapes)
        //{
        //    shape.gameObject.SetActive(false);
        //    Debug.Log($"Disabled {shape.gameObject.name}");
        //}
        lastFrameTime = DateTime.Now;
        multiSource2MatHelper.Initialize();

    }

    [Conditional("STANDARD_DETECTION")]
    private void InitQrDetector()
    {
        detector = new QRCodeDetector();
    }

    [Conditional("WECHAT_DETECTION")]
    private void InitWeChatDetector()
    {
        wechatDetector = new WeChatQRCode();
    }

    IEnumerator DetectionCoroutine()
    {
        //Debug.Log("Coroutine started");
        var task = Task.Run(() => { WeChatDetection(); StandardDetection(); });
        yield return new WaitUntil(() => task.IsCompleted);
        coroutine = null;
        //Debug.Log("Coroutine completed");
    }

    /// <summary>
    /// Raises the source to mat helper initialized event.
    /// </summary>
    public void OnSourceToMatHelperInitialized()
    {
        Debug.Log("OnSourceToMatHelperInitialized");

        Mat rgbaMat = multiSource2MatHelper.GetMat();

        texture = new Texture2D(rgbaMat.cols(), rgbaMat.rows(), TextureFormat.RGBA32, false);
        Utils.matToTexture2D(rgbaMat, texture);

        //resultPreview.texture = texture;
        //resultPreview.GetComponent<AspectRatioFitter>().aspectRatio = (float)texture.width / texture.height;

        // Set the Texture2D as the main texture of the Renderer component attached to the game object
        gameObject.GetComponent<Renderer>().material.mainTexture = texture;

        // Adjust the scale of the game object to match the dimensions of the texture
        gameObject.transform.localScale = new Vector3(rgbaMat.cols(), rgbaMat.rows(), 1);
        Debug.Log("Screen.width " + Screen.width + " Screen.height " + Screen.height + " Screen.orientation " + Screen.orientation);

        // Adjust the orthographic size of the main Camera to fit the aspect ratio of the image
        float width = rgbaMat.width();
        float height = rgbaMat.height();
        float imageSizeScale = 1.0f;
        float widthScale = (float)Screen.width / width;
        float heightScale = (float)Screen.height / height;
        if (widthScale < heightScale)
        {
            Camera.main.orthographicSize = (width * (float)Screen.height / (float)Screen.width) / 2;
            imageSizeScale = (float)Screen.height / (float)Screen.width;
        }
        else
        {
            Camera.main.orthographicSize = height / 2;
        }

        if (fpsMonitor != null)
        {
            fpsMonitor.Add("width", rgbaMat.width().ToString());
            fpsMonitor.Add("height", rgbaMat.height().ToString());
            fpsMonitor.Add("orientation", Screen.orientation.ToString());
        }

        // set camera parameters.
        double fx;
        double fy;
        double cx;
        double cy;

        int max_d = (int)Mathf.Max(width, height);
        fx = max_d;
        fy = max_d;
        cx = width / 2.0f;
        cy = height / 2.0f;

        camMatrix = new Mat(3, 3, CvType.CV_64FC1);
        camMatrix.put(0, 0, fx);
        camMatrix.put(0, 1, 0);
        camMatrix.put(0, 2, cx);
        camMatrix.put(1, 0, 0);
        camMatrix.put(1, 1, fy);
        camMatrix.put(1, 2, cy);
        camMatrix.put(2, 0, 0);
        camMatrix.put(2, 1, 0);
        camMatrix.put(2, 2, 1.0f);

        distCoeffs = new MatOfDouble(0, 0, 0, 0);

        Size imageSize = new Size(width * imageSizeScale, height * imageSizeScale);
        double apertureWidth = 0;
        double apertureHeight = 0;
        double[] fovx = new double[1];
        double[] fovy = new double[1];
        double[] focalLength = new double[1];
        Point principalPoint = new Point(0, 0);
        double[] aspectratio = new double[1];

        Calib3d.calibrationMatrixValues(camMatrix, imageSize, apertureWidth, apertureHeight, fovx, fovy, focalLength, principalPoint, aspectratio);

        // To convert the difference of the FOV value of the OpenCV and Unity. 
        double fovXScale = (2.0 * Mathf.Atan((float)(imageSize.width / (2.0 * fx)))) / (Mathf.Atan2((float)cx, (float)fx) + Mathf.Atan2((float)(imageSize.width - cx), (float)fx));
        double fovYScale = (2.0 * Mathf.Atan((float)(imageSize.height / (2.0 * fy)))) / (Mathf.Atan2((float)cy, (float)fy) + Mathf.Atan2((float)(imageSize.height - cy), (float)fy));

        grayMat = new Mat(rgbaMat.rows(), rgbaMat.cols(), CvType.CV_8UC1);

        points = new Mat();
        decodedInfo = new List<string>();
        straightQrcode = new List<Mat>();

#if !OPENCV_DONT_USE_WEBCAMTEXTURE_API
        // If the WebCam is front facing, flip the Mat horizontally. Required for successful detection.
        if (multiSource2MatHelper.source2MatHelper is WebCamTexture2MatHelper webCamHelper)
            webCamHelper.flipHorizontal = webCamHelper.IsFrontFacing();
#endif
        arHelper.SetCamMatrix(camMatrix);
        arHelper.SetDistCoeffs(distCoeffs);
        arHelper.Initialize(Screen.width, Screen.height, rgbaMat.width(), rgbaMat.height());
    }

    /// <summary>
    /// Raises the source to mat helper disposed event.
    /// </summary>
    public void OnSourceToMatHelperDisposed()
    {

        if (grayMat != null)
            grayMat.Dispose();

        if (texture != null)
        {
            Destroy(texture);
            texture = null;
        }

        if (points != null)
            points.Dispose();

        if (decodedInfo != null)
            decodedInfo.Clear();

        if (straightQrcode != null)
            foreach (var item in straightQrcode)
            {
                item.Dispose();
            }
        if (arHelper != null)
            arHelper.Dispose();
    }

    /// <summary>
    /// Raises the source to mat helper error occurred event.
    /// </summary>
    /// <param name="errorCode">Error code.</param>
    /// <param name="message">Message.</param>
    public void OnSourceToMatHelperErrorOccurred(Source2MatHelperErrorCode errorCode, string message)
    {
        Debug.Log("OnSourceToMatHelperErrorOccurred " + errorCode + ":" + message);

        if (fpsMonitor != null)
        {
            fpsMonitor.consoleText = "ErrorCode: " + errorCode + ":" + message;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!multiSource2MatHelper.IsPlaying() || !multiSource2MatHelper.DidUpdateThisFrame())
        {
            return;
        }

        Mat rgbaMat = multiSource2MatHelper.GetMat();

        Imgproc.cvtColor(rgbaMat, grayMat, Imgproc.COLOR_RGBA2GRAY);
        //Imgproc.medianBlur(grayMat, grayMat, 5);
        //Imgproc.cvtColor(grayMat,rgbaMat, Imgproc.COLOR_GRAY2RGBA);


        //Time increment for each QrMarker
        if (lastFrameTime == null)
        {
            deltaTime = Time.unscaledDeltaTime;
            lastFrameTime = DateTime.Now;
        }
        else
        {
            var delta = DateTime.Now - lastFrameTime;
            lastFrameTime = DateTime.Now;
            deltaTime = (float)(delta.TotalMilliseconds / 1000);
        }

        if (detectionAck)
        {
            coroutine = StartCoroutine(DetectionCoroutine());
            detectionAck = false;
        }

        if (coroutine == null && result)
        {
            for (int i = 0; i < Math.Min(pointSetList.Count, decodedInfo.Count); i++)
            {
                if (string.IsNullOrEmpty(decodedInfo[i]))
                {
                    continue;
                }
                if (pointSetList[i] == null)
                {
                    continue;
                }
                float[] points_arr = new float[8];
                // var test = new Mat(2, 2, CvType.CV_32FC2);
                pointSetList[i].get(0, 0, points_arr);

                using MatOfPoint3f objectPoints = new MatOfPoint3f(
                    new Point3(-markerLength / 2f, markerLength / 2f, 0),
                    new Point3(markerLength / 2f, markerLength / 2f, 0),
                    new Point3(markerLength / 2f, -markerLength / 2f, 0),
                    new Point3(-markerLength / 2f, -markerLength / 2f, 0)
                    );
                var content = decodedInfo[i];
                if (!arHelper.markers.ContainsKey(content))
                {
                    arHelper.markers.Add(content, new QrMarker());
                }
                var marker = arHelper.markers[decodedInfo[i]];
                marker.ImagePoints = new Vector2[4]
                {
                                        new(points_arr[0], points_arr[1]),
                                        new(points_arr[2], points_arr[3]),
                                        new(points_arr[4], points_arr[5]),
                                        new(points_arr[6], points_arr[7])
                };
                marker.ObjectPoints = objectPoints.toVector3Array();
                marker.TimeFromLastUpdate = 0;
            }
        }
        // else
        // {
        //     var delta = DateTime.Now - lastFrame;
        //     if (delta.TotalMilliseconds > QrMarker.UpdateTimeLimit)
        //     {
        //         Imgproc.putText(rgbaMat, "Decoding failed.", new Point(5, rgbaMat.rows() - 10), Imgproc.FONT_HERSHEY_SIMPLEX, 0.7, new Scalar(255, 255, 255, 255), 2, Imgproc.LINE_AA, false);
        //     }
        // }

        if (coroutine == null)
        {
            detectionAck = true;
        }

        var keysToRemove = new List<string>();
        //remove oudated QrMarkers
        foreach (var item in arHelper.markers)
        {
            var key = item.Key;
            var marker = item.Value;
            if (marker.TimeFromLastUpdate > QrMarker.UpdateTimeLimit)
                if (marker.TimeFromLastUpdate > QrMarker.UpdateTimeLimit)
                {
                    marker.Dispose();
                    keysToRemove.Add(key);
                }
        }

        foreach (var key in keysToRemove)
        {
            arHelper.markers[key].Dispose();
            arHelper.markers.Remove(key);
        }

        foreach (var item in arHelper.markers)
        {
            item.Value.AddTime(deltaTime: deltaTime);
        }

        Finalize(rgbaMat);


    }

    void Finalize(Mat rgbaMat)
    {
        Utils.matToTexture2D(rgbaMat, texture);
    }

    [Conditional("WECHAT_DETECTION")]
    private void WeChatDetection()
    {

        foreach (var pointSet in pointSetList)
        {
            pointSet.Dispose();
        }
        decodedInfo = wechatDetector.detectAndDecode(grayMat, pointSetList);



        foreach (var pointSet in pointSetList)
        {
            float[] points_arr = new float[8];
            try
            {
                pointSet.get(0, 0, points_arr);
                var x1 = points_arr[0] - points_arr[4];
                var y1 = points_arr[1] - points_arr[5];
                var x2 = points_arr[2] - points_arr[6];
                var y2 = points_arr[3] - points_arr[7];
                if (x1 * y2 - x2 * y1 < 0)
                {
                    (points_arr[2], points_arr[6]) = (points_arr[6], points_arr[2]);
                    (points_arr[3], points_arr[7]) = (points_arr[7], points_arr[3]);
                }
                pointSet.put(0, 0, points_arr);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        result = (decodedInfo != null) && (decodedInfo.Count > 0);
    }

    [Conditional("STANDARD_DETECTION")]
    private void StandardDetection()
    {
        result = detector.detectAndDecodeMulti(grayMat, decodedInfo, points, straightQrcode);
        if (result)
        {
            for (int i = 0; i < points.rows(); i++)
            {
                float[] points_arr = new float[8];
                points.get(i, 0, points_arr);
                if (pointSetList.Count <= i)
                {
                    pointSetList.Add(new Mat((4, 1), CvType.CV_32FC2));
                }
                pointSetList[i].put(0, 0, points_arr);
                if (decodedInfo.Count <= i)
                {
                    decodedInfo.Add("");
                }
            }
        }
    }

    /// <summary>
    /// Raises the destroy event.
    /// </summary>
    void OnDestroy()
    {
        multiSource2MatHelper.Dispose();

        if (detector != null)
            detector.Dispose();
    }
}