using System;
using System.Collections.Generic;
using UnityEngine;


// OpenCVForUnity.UnityUtils là thư viện cầu nối giữa Unity và OpenCV. Nó chứa các hàm tiện ích hỗ trợ:
// Chuyển đổi hình ảnh giữa Texture2D (Unity) và Mat (OpenCV).
// Vẽ ảnh OpenCV (Mat) ra texture Unity để hiển thị.
// Ghi log, quản lý file, hoặc các tác vụ tiện ích khác phục vụ tích hợp giữa hai hệ thống.

namespace OpenCVForUnity.UnityUtils
{
    public struct PoseData
    {
        public Vector3 pos;
        public Quaternion rot;
    }

    /// <summary>
    /// AR utilities.
    /// </summary>
    public class ARUtils
    {
        /// <summary>
        /// Convertes rvec value to rotation transform.
        /// </summary>
        /// <param name="tvec">Rvec.</param>
        /// <returns>Rotation.</returns>

        //? Chuyển đổi rvec (vector quay) từ OpenCV sang Unity
        public static Quaternion ConvertRvecToRot(IList<double> rvec)
        {
            if (rvec == null)
                throw new ArgumentNullException("rvec");
            if (rvec.Count < 3)
                throw new ArgumentException("rvec.Count < 3");

            Vector3 _rvec = new Vector3((float)rvec[0], (float)rvec[1], (float)rvec[2]);
            float theta = _rvec.magnitude;
            _rvec.Normalize();

            // http://stackoverflow.com/questions/12933284/rodrigues-into-eulerangles-and-vice-versa
            return Quaternion.AngleAxis(theta * Mathf.Rad2Deg, _rvec);
        }

        /// <summary>
        /// Convertes tvec value to position transform.
        /// </summary>
        /// <param name="tvec">Tvec.</param>
        /// <returns>Position.</returns>

        //? Chuyển đổi tvec (vector dịch chuyển) từ OpenCV sang Unity
        public static Vector3 ConvertTvecToPos(IList<double> tvec)
        {
            if (tvec == null)
                throw new ArgumentNullException("tvec");
            if (tvec.Count < 3)
                throw new ArgumentException("tvec.Count < 3");

            if (tvec.Count < 3)
                return new Vector3();

            return new Vector3((float)tvec[0], (float)tvec[1], (float)tvec[2]);
        }

        /// <summary>
        /// Convertes rvec and tvec value to PoseData.
        /// </summary>
        /// <param name="tvec">Rvec.</param>
        /// <param name="tvec">Tvec.</param>
        /// <returns>PoseData.</returns>

        //? Chuyển đổi rvec và tvec từ OpenCV sang PoseData (dữ liệu vị trí và góc quay)
        //? => Gộp 2 bước trên
        public static PoseData ConvertRvecTvecToPoseData(IList<double> rvec, IList<double> tvec)
        {
            PoseData data = new PoseData();
            data.pos = ConvertTvecToPos(tvec);
            data.rot = ConvertRvecToRot(rvec);

            return data;
        }

        private static Vector3 vector_one = Vector3.one;
        private static Matrix4x4 invertYMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, -1, 1));

        /// <summary>
        /// Convertes PoseData to transform matrix.
        /// </summary>
        /// <param name="posedata">PoseData.</param>
        /// <param name="toLeftHandCoordinateSystem">Determines if convert the transformation matrix to the left-hand coordinate system.</param>
        /// <returns>Transform matrix.</returns>

        //? Chuyển đổi PoseData (dữ liệu vị trí và góc quay) sang ma trận biến đổi 4x4 TRS
        //? => TRS là view tắt của Translation (dịch chuyển), Rotation (quay) và Scale (tỷ lệ)
        //? => Để sử dụng trong Unity 
        public static Matrix4x4 ConvertPoseDataToMatrix(ref PoseData poseData, bool toLeftHandCoordinateSystem = false)
        {
            Matrix4x4 matrix = Matrix4x4.TRS(poseData.pos, poseData.rot, vector_one);

            // right-handed coordinates system (OpenCV) to left-handed one (Unity)
            // https://stackoverflow.com/questions/30234945/change-handedness-of-a-row-major-4x4-transformation-matrix
            if (toLeftHandCoordinateSystem)
                matrix = invertYMatrix * matrix * invertYMatrix;

            return matrix;
        }

        /// <summary>
        /// Convertes transform matrix to PoseData.
        /// </summary>
        /// <param name="matrix">Transform matrix.</param>
        /// <returns>PoseData.</returns>


        //? Chuyển đổi ma trận biến đổi 4x4 TRS sang PoseData (dữ liệu vị trí và góc quay)
        //? => Để sử dụng trong OpenCV
        public static PoseData ConvertMatrixToPoseData(ref Matrix4x4 matrix)
        {
            PoseData data = new PoseData();
            data.pos = ExtractTranslationFromMatrix(ref matrix);
            data.rot = ExtractRotationFromMatrix(ref matrix);

            return data;
        }

        /// <summary>
        /// Creates pose data dictionary.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <param name="rvecs">Rvecs.</param>
        /// <param name="tvecs">Tvecs.</param>
        /// <returns>PoseData dictionary.</returns>

        public static Dictionary<int, PoseData> CreatePoseDataDict(IList<int> ids, IList<double> rvecs, IList<double> tvecs)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");
            if (rvecs == null)
                throw new ArgumentNullException("rvecs");
            if (tvecs == null)
                throw new ArgumentNullException("tvecs");

            Dictionary<int, PoseData> dict = new Dictionary<int, PoseData>();
            if (ids.Count == 0 || ids.Count * 3 != rvecs.Count || ids.Count * 3 != tvecs.Count)
                return dict;

            Vector3 rvec = new Vector3();
            for (int i = 0; i < ids.Count; i++)
            {
                PoseData data = new PoseData();
                data.pos.Set((float)tvecs[i * 3], (float)tvecs[i * 3 + 1], (float)tvecs[i * 3 + 2]);

                rvec.Set((float)rvecs[i * 3], (float)rvecs[i * 3 + 1], (float)rvecs[i * 3 + 2]);
                float theta = rvec.magnitude;
                rvec.Normalize();
                data.rot = Quaternion.AngleAxis(theta * Mathf.Rad2Deg, rvec);

                dict[ids[i]] = data;
            }
            return dict;
        }

        /// <summary>
        /// Performs a lowpass check on the position and rotation in newPose, comparing them to oldPose.
        /// </summary>
        /// <param name="oldPose">Old PoseData.</param>
        /// <param name="newPose">New PoseData.</param>
        /// <param name="posThreshold">Positon threshold.</param>
        /// <param name="rotThreshold">Rotation threshold.</param>

        //? Bộ lọc thông thấp => Làm mượt mà dữ liệu vị trí và góc quay của PoseData
        //? => Nếu vị trí và góc quay mới không thay đổi nhiều so với cũ thì giữ nguyên giá trị cũ
        //? => Nếu thay đổi nhiều thì giữ nguyên giá trị mới
        //? => Để sử dụng trong OpenCV
        public static void LowPassPoseData(ref PoseData oldPose, ref PoseData newPose, float posThreshold, float rotThreshold)
        {
            posThreshold *= posThreshold;

            float posDiff = (newPose.pos - oldPose.pos).sqrMagnitude;
            float rotDiff = Quaternion.Angle(newPose.rot, oldPose.rot);

            if (posDiff < posThreshold)
            {
                newPose.pos = oldPose.pos;
            }

            if (rotDiff < rotThreshold)
            {
                newPose.rot = oldPose.rot;
            }
        }

        /// <summary>
        /// Performs a lowpass check on the position and rotation of each marker in newDict, comparing them to those in oldDict.
        /// </summary>
        /// <param name="oldDict">Old dictionary.</param>
        /// <param name="newDict">New dictionary.</param>
        /// <param name="posThreshold">Positon threshold.</param>
        /// <param name="rotThreshold">Rotation threshold.</param>

        //? Dùng trong trường hợp nhiều marker (nhiều mã QR)
        //  Bộ lọc thông thấp => Làm mượt mà dữ liệu vị trí và góc quay của PoseData
        //? => Nếu vị trí và góc quay mới không thay đổi nhiều so với cũ thì giữ nguyên giá trị cũ
        //? => Nếu thay đổi nhiều thì giữ nguyên giá trị mới
        //? => Để sử dụng trong OpenCV
        public static void LowPassPoseDataDict(Dictionary<int, PoseData> oldDict, Dictionary<int, PoseData> newDict, float posThreshold, float rotThreshold)
        {
            if (oldDict == null)
                throw new ArgumentNullException("oldDict");
            if (newDict == null)
                throw new ArgumentNullException("newDict");

            posThreshold *= posThreshold;

            List<int> keys = new List<int>(newDict.Keys);
            foreach (int key in keys)
            {
                if (!oldDict.ContainsKey(key))
                    continue;

                PoseData oldPose = oldDict[key];
                PoseData newPose = newDict[key];

                float posDiff = (newPose.pos - oldPose.pos).sqrMagnitude;
                float rotDiff = Quaternion.Angle(newPose.rot, oldPose.rot);

                if (posDiff < posThreshold)
                {
                    newPose.pos = oldPose.pos;
                }

                if (rotDiff < rotThreshold)
                {
                    newPose.rot = oldPose.rot;
                }

                newDict[key] = newPose;
            }
        }


        /// <summary>
        /// Extract translation from transform matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <returns>
        /// Translation offset.
        /// </returns>

        //? Trích xuất vị trí từ ma trận biến đổi 4x4 TRS
        //? => Để sử dụng trong OpenCV
        public static Vector3 ExtractTranslationFromMatrix(ref Matrix4x4 matrix)
        {
            Vector3 translate;
            translate.x = matrix.m03;
            translate.y = matrix.m13;
            translate.z = matrix.m23;
            return translate;
        }

        /// <summary>
        /// Extract rotation quaternion from transform matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <returns>
        /// Quaternion representation of rotation transform.
        /// </returns>

        //? Trích xuất góc quay từ ma trận biến đổi 4x4 TRS
        //? => Để sử dụng trong OpenCV

        public static Quaternion ExtractRotationFromMatrix(ref Matrix4x4 matrix)
        {
            Vector3 forward;
            forward.x = matrix.m02;
            forward.y = matrix.m12;
            forward.z = matrix.m22;

            Vector3 upwards;
            upwards.x = matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = matrix.m21;

            return Quaternion.LookRotation(forward, upwards);
        }

        /// <summary>
        /// Extract scale from transform matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <returns>
        /// Scale vector.
        /// </returns>

        //? Trích xuất tỷ lệ (Scale) từ ma trận biến đổi 4x4 TRS
        //? => Để sử dụng trong OpenCV
        public static Vector3 ExtractScaleFromMatrix(ref Matrix4x4 matrix)
        {
            Vector3 scale;
            scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
            scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
            scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;

            if (Vector3.Cross(matrix.GetColumn(0), matrix.GetColumn(1)).normalized != (Vector3)matrix.GetColumn(2).normalized)
            {
                scale.x *= -1;
            }

            return scale;
        }

        /// <summary>
        /// Compose TRS matrix from position, rotation and scale.
        /// </summary>
        /// <param name="localPosition">Position.</param>
        /// <param name="localRotation">Rotation.</param>
        /// <param name="localScale">Scale.</param>
        /// <param name="matrix">Transform matrix.</param>
        public static void ComposeMatrix(Vector3 localPosition, Quaternion localRotation, Vector3 localScale, out Matrix4x4 matrix)
        {
            matrix = Matrix4x4.TRS(localPosition, localRotation, localScale);
        }

        /// <summary>
        /// Extract position, rotation and scale from TRS matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <param name="localPosition">Position.</param>
        /// <param name="localRotation">Rotation.</param>
        /// <param name="localScale">Scale.</param>
        public static void DecomposeMatrix(ref Matrix4x4 matrix, out Vector3 localPosition, out Quaternion localRotation, out Vector3 localScale)
        {
            localPosition = ExtractTranslationFromMatrix(ref matrix);
            localRotation = ExtractRotationFromMatrix(ref matrix);
            localScale = ExtractScaleFromMatrix(ref matrix);
        }

        /// <summary>
        /// Set transform component from TRS matrix.
        /// </summary>
        /// <param name="transform">Transform component.</param>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>

        // ? Thiết lập vị trí, góc quay và tỷ lệ của GameObject từ ma trận biến đổi 4x4 TRS
        //? Đặt transform từ ma trận
        // ? => Để sử dụng trong OpenCV
        //? Dùng trong QrMarker.UpdateTransform()
        public static void SetTransformFromMatrix(Transform transform, ref Matrix4x4 matrix)
        {
            transform.localPosition = ExtractTranslationFromMatrix(ref matrix);
            transform.localRotation = ExtractRotationFromMatrix(ref matrix);
            transform.localScale = ExtractScaleFromMatrix(ref matrix);
        }

        /// <summary>
        /// Calculate projection matrix from camera matrix values. (OpenCV coordinate system to OpenGL coordinate system)
        /// </summary>
        /// <param name="fx">Focal length x.</param>
        /// <param name="fy">Focal length y.</param>
        /// <param name="cx">Image center point x.(principal point x)</param>
        /// <param name="cy">Image center point y.(principal point y)</param>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="near">The near clipping plane distance.</param>
        /// <param name="far">The far clipping plane distance.</param>
        /// <returns>
        /// Projection matrix.
        /// </returns>

        ///? Tính toán ma trận chiếu từ các giá trị ma trận camera
        /// //? => Để sử dụng trong OpenCV
        /// //? => fx, fy là tiêu cự của camera
        /// //? => cx, cy là điểm chính của camera
        /// //? => width, height là kích thước của ảnh
        /// //? => near, far là khoảng cách gần và xa của mặt phẳng cắt
        /// //? => Để sử dụng trong OpenGL
        public static Matrix4x4 CalculateProjectionMatrixFromCameraMatrixValues(float fx, float fy, float cx, float cy, float width, float height, float near, float far)
        {
            Matrix4x4 projectionMatrix = new Matrix4x4();
            projectionMatrix.m00 = 2.0f * fx / width;
            projectionMatrix.m02 = 1.0f - 2.0f * cx / width;
            projectionMatrix.m11 = 2.0f * fy / height;
            projectionMatrix.m12 = -1.0f + 2.0f * cy / height;
            projectionMatrix.m22 = -(far + near) / (far - near);
            projectionMatrix.m23 = -2.0f * far * near / (far - near);
            projectionMatrix.m32 = -1.0f;

            return projectionMatrix;
        }

        /// <summary>
        /// Calculate camera matrix values from projection matrix. (OpenGL coordinate system to OpenCV coordinate system)
        /// </summary>
        /// <param name="projectionMatrix">Projection matrix.</param>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="fovV">Vertical field of view.</param>
        /// <returns>
        /// Camera matrix values. (fx = matrx.m00, fy = matrx.m11, cx = matrx.m02, cy = matrx.m12)
        /// </returns>

        ///? Tính toán các giá trị ma trận camera từ ma trận chiếu
        /// //? => Để sử dụng trong OpenCV
        /// //? => matrx.m00, matrx.m11 là tiêu cự của camera
        /// //? => matrx.m02, matrx.m12 là điểm chính của camera
        /// //? => width, height là kích thước của ảnh
        /// //? => fovV là góc nhìn dọc của camera
        /// //? => Để sử dụng trong OpenGL
        public static Matrix4x4 CameraMatrixValuesFromCalculateProjectionMatrix(Matrix4x4 projectionMatrix, float width, float height, float fovV)
        {
            float fovH = 2.0f * Mathf.Atan(width / height * Mathf.Tan(fovV * Mathf.Deg2Rad / 2.0f)) * Mathf.Rad2Deg;

            Matrix4x4 cameraMatrix = new Matrix4x4();
            cameraMatrix.m00 = CalculateDistance(width, fovH);
            cameraMatrix.m02 = -((projectionMatrix.m02 * width - width) / 2);
            cameraMatrix.m11 = CalculateDistance(height, fovV);
            cameraMatrix.m12 = (projectionMatrix.m12 * height + height) / 2;
            cameraMatrix.m22 = 1.0f;

            return cameraMatrix;
        }

        /// <summary>
        /// Calculate frustum size.
        /// https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        /// </summary>
        /// <param name="distance">Distance.</param>
        /// <param name="fov">Field of view. (horizontal or vertical direction)</param>
        /// <returns>
        /// Frustum height.
        /// </returns>
        public static float CalculateFrustumSize(float distance, float fov)
        {
            return 2.0f * distance * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Calculate distance.
        /// https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        /// </summary>
        /// <param name="frustumHeight">One side size of a frustum.</param>
        /// <param name="fov">Field of view. (horizontal or vertical direction)</param>
        /// <returns>
        /// Distance.
        /// </returns>
        public static float CalculateDistance(float frustumSize, float fov)
        {
            return frustumSize * 0.5f / Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Calculate FOV angle.
        /// https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        /// </summary>
        /// <param name="frustumHeight">One side size of a frustum.</param>
        /// <param name="distance">Distance.</param>
        /// <returns>
        /// FOV angle.
        /// </returns>
        public static float CalculateFOVAngle(float frustumSize, float distance)
        {
            return 2.0f * Mathf.Atan(frustumSize * 0.5f / distance) * Mathf.Rad2Deg;
        }
    }
}