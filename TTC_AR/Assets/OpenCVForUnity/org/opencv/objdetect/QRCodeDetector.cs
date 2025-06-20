
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OpenCVForUnity .ObjdetectModule
{

    // C++: class QRCodeDetector


    public class QRCodeDetector : GraphicalCodeDetector
    {

        protected override void Dispose(bool disposing)
        {

            try
            {
                if (disposing)
                {
                }
                if (IsEnabledDispose)
                {
                    if (nativeObj != IntPtr.Zero)
                        objdetect_QRCodeDetector_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal QRCodeDetector(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new QRCodeDetector __fromPtr__(IntPtr addr) { return new QRCodeDetector(addr); }

        //
        // C++:   cv::QRCodeDetector::QRCodeDetector()
        //

        public QRCodeDetector() :
            base(ThrowIfNullIntPtr(objdetect_QRCodeDetector_QRCodeDetector_10()))
        {



        }


        //
        // C++:  QRCodeDetector cv::QRCodeDetector::setEpsX(double epsX)
        //

        /// <summary>
        ///  sets the epsilon used during the horizontal scan of QR code stop marker detection.
        /// </summary>
        /// <param name="epsX">
        /// Epsilon neighborhood, which allows you to determine the horizontal pattern
        ///       of the scheme 1:1:3:1:1 according to QR code standard.
        /// </param>
        public QRCodeDetector setEpsX(double epsX)
        {
            ThrowIfDisposed();

            return new QRCodeDetector(DisposableObject.ThrowIfNullIntPtr(objdetect_QRCodeDetector_setEpsX_10(nativeObj, epsX)));


        }


        //
        // C++:  QRCodeDetector cv::QRCodeDetector::setEpsY(double epsY)
        //

        /// <summary>
        ///  sets the epsilon used during the vertical scan of QR code stop marker detection.
        /// </summary>
        /// <param name="epsY">
        /// Epsilon neighborhood, which allows you to determine the vertical pattern
        ///       of the scheme 1:1:3:1:1 according to QR code standard.
        /// </param>
        public QRCodeDetector setEpsY(double epsY)
        {
            ThrowIfDisposed();

            return new QRCodeDetector(DisposableObject.ThrowIfNullIntPtr(objdetect_QRCodeDetector_setEpsY_10(nativeObj, epsY)));


        }


        //
        // C++:  QRCodeDetector cv::QRCodeDetector::setUseAlignmentMarkers(bool useAlignmentMarkers)
        //

        /// <summary>
        ///  use markers to improve the position of the corners of the QR code
        /// </summary>
        /// <remarks>
        ///         alignmentMarkers using by default
        /// </remarks>
        public QRCodeDetector setUseAlignmentMarkers(bool useAlignmentMarkers)
        {
            ThrowIfDisposed();

            return new QRCodeDetector(DisposableObject.ThrowIfNullIntPtr(objdetect_QRCodeDetector_setUseAlignmentMarkers_10(nativeObj, useAlignmentMarkers)));


        }


        //
        // C++:  String cv::QRCodeDetector::decodeCurved(Mat img, Mat points, Mat& straight_qrcode = Mat())
        //

        /// <summary>
        ///  Decodes QR code on a curved surface in image once it's found by the detect() method.
        /// </summary>
        /// <remarks>
        ///       Returns UTF8-encoded output string or empty string if the code cannot be decoded.
        /// </remarks>
        /// <param name="img">
        /// grayscale or color (BGR) image containing QR code.
        /// </param>
        /// <param name="points">
        /// Quadrangle vertices found by detect() method (or some other algorithm).
        /// </param>
        /// <param name="straight_qrcode">
        /// The optional output image containing rectified and binarized QR code
        /// </param>
        public string decodeCurved(Mat img, Mat points, Mat straight_qrcode)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            if (straight_qrcode != null) straight_qrcode.ThrowIfDisposed();

            string retVal = Marshal.PtrToStringAnsi(DisposableObject.ThrowIfNullIntPtr(objdetect_QRCodeDetector_decodeCurved_10(nativeObj, img.nativeObj, points.nativeObj, straight_qrcode.nativeObj)));

            return retVal;
        }

        /// <summary>
        ///  Decodes QR code on a curved surface in image once it's found by the detect() method.
        /// </summary>
        /// <remarks>
        ///       Returns UTF8-encoded output string or empty string if the code cannot be decoded.
        /// </remarks>
        /// <param name="img">
        /// grayscale or color (BGR) image containing QR code.
        /// </param>
        /// <param name="points">
        /// Quadrangle vertices found by detect() method (or some other algorithm).
        /// </param>
        /// <param name="straight_qrcode">
        /// The optional output image containing rectified and binarized QR code
        /// </param>
        public string decodeCurved(Mat img, Mat points)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();

            string retVal = Marshal.PtrToStringAnsi(DisposableObject.ThrowIfNullIntPtr(objdetect_QRCodeDetector_decodeCurved_11(nativeObj, img.nativeObj, points.nativeObj)));

            return retVal;
        }


        //
        // C++:  string cv::QRCodeDetector::detectAndDecodeCurved(Mat img, Mat& points = Mat(), Mat& straight_qrcode = Mat())
        //

        /// <summary>
        ///  Both detects and decodes QR code on a curved surface
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing QR code.
        /// </param>
        /// <param name="points">
        /// optional output array of vertices of the found QR code quadrangle. Will be empty if not found.
        /// </param>
        /// <param name="straight_qrcode">
        /// The optional output image containing rectified and binarized QR code
        /// </param>
        public string detectAndDecodeCurved(Mat img, Mat points, Mat straight_qrcode)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();
            if (straight_qrcode != null) straight_qrcode.ThrowIfDisposed();

            string retVal = Marshal.PtrToStringAnsi(DisposableObject.ThrowIfNullIntPtr(objdetect_QRCodeDetector_detectAndDecodeCurved_10(nativeObj, img.nativeObj, points.nativeObj, straight_qrcode.nativeObj)));

            return retVal;
        }

        /// <summary>
        ///  Both detects and decodes QR code on a curved surface
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing QR code.
        /// </param>
        /// <param name="points">
        /// optional output array of vertices of the found QR code quadrangle. Will be empty if not found.
        /// </param>
        /// <param name="straight_qrcode">
        /// The optional output image containing rectified and binarized QR code
        /// </param>
        public string detectAndDecodeCurved(Mat img, Mat points)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();
            if (points != null) points.ThrowIfDisposed();

            string retVal = Marshal.PtrToStringAnsi(DisposableObject.ThrowIfNullIntPtr(objdetect_QRCodeDetector_detectAndDecodeCurved_11(nativeObj, img.nativeObj, points.nativeObj)));

            return retVal;
        }

        /// <summary>
        ///  Both detects and decodes QR code on a curved surface
        /// </summary>
        /// <param name="img">
        /// grayscale or color (BGR) image containing QR code.
        /// </param>
        /// <param name="points">
        /// optional output array of vertices of the found QR code quadrangle. Will be empty if not found.
        /// </param>
        /// <param name="straight_qrcode">
        /// The optional output image containing rectified and binarized QR code
        /// </param>
        public string detectAndDecodeCurved(Mat img)
        {
            ThrowIfDisposed();
            if (img != null) img.ThrowIfDisposed();

            string retVal = Marshal.PtrToStringAnsi(DisposableObject.ThrowIfNullIntPtr(objdetect_QRCodeDetector_detectAndDecodeCurved_12(nativeObj, img.nativeObj)));

            return retVal;
        }


#if (UNITY_IOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:   cv::QRCodeDetector::QRCodeDetector()
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_QRCodeDetector_QRCodeDetector_10();

        // C++:  QRCodeDetector cv::QRCodeDetector::setEpsX(double epsX)
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_QRCodeDetector_setEpsX_10(IntPtr nativeObj, double epsX);

        // C++:  QRCodeDetector cv::QRCodeDetector::setEpsY(double epsY)
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_QRCodeDetector_setEpsY_10(IntPtr nativeObj, double epsY);

        // C++:  QRCodeDetector cv::QRCodeDetector::setUseAlignmentMarkers(bool useAlignmentMarkers)
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_QRCodeDetector_setUseAlignmentMarkers_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool useAlignmentMarkers);

        // C++:  String cv::QRCodeDetector::decodeCurved(Mat img, Mat points, Mat& straight_qrcode = Mat())
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_QRCodeDetector_decodeCurved_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj, IntPtr straight_qrcode_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_QRCodeDetector_decodeCurved_11(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj);

        // C++:  string cv::QRCodeDetector::detectAndDecodeCurved(Mat img, Mat& points = Mat(), Mat& straight_qrcode = Mat())
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_QRCodeDetector_detectAndDecodeCurved_10(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj, IntPtr straight_qrcode_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_QRCodeDetector_detectAndDecodeCurved_11(IntPtr nativeObj, IntPtr img_nativeObj, IntPtr points_nativeObj);
        [DllImport(LIBNAME)]
        private static extern IntPtr objdetect_QRCodeDetector_detectAndDecodeCurved_12(IntPtr nativeObj, IntPtr img_nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void objdetect_QRCodeDetector_delete(IntPtr nativeObj);

    }
}
