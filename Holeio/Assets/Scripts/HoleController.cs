using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vasik
{
    public class HoleController : MonoBehaviour
    {
        public MeshFilter meshFilter;
        public MeshCollider meshCollider;

        public float radius = 0.0f;
        public float speed = 5f;
        public float centerSpeedRatio = 9f;
        public Transform holeCenter;

        [Space]
        public Vector3 minLimits;
        public Vector3 maxLimits;

        private Mesh mesh;

        private List<int> holeVertices;
  //      private List<Vector3> offsets;

        private Matrix4x4 localToWorld;
        private Vector3 currentVertexPosition;

        private float touchX, touchY = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            holeVertices = new List<int>();
 //           offsets = new List<Vector3>();
            localToWorld = transform.localToWorldMatrix;
            mesh = meshFilter.mesh;

            GetHoleVertices();
        }

        // Update is called once per frame
        void Update()
        {
            HoleMovement();
            UpdateHoleVerticesPosition();
        }

        private void HoleMovement()
        {
            if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                MobileControl();
            }
            else
            {
                NonMobileControl();
            }
        }

        private void MobileControl()
        {
            if (Input.touchCount > 0)
            {
                touchX = Input.GetTouch(0).deltaPosition.x;
                touchY = Input.GetTouch(0).deltaPosition.y;

                for (int i = 0; i < holeVertices.Count; i++)
                {
                    //Vector3[] vertices = mesh.vertices;
                    //vertices[holeVertices[i]] = new Vector3(vertices[holeVertices[i]].x - touchX * (speed * Time.deltaTime),
                    //    vertices[holeVertices[i]].y + touchY * ((speed / 2f) * Time.deltaTime), vertices[holeVertices[i]].z);

                    Vector3[] vertices = mesh.vertices;
                    vertices[holeVertices[i]] = new Vector3(vertices[holeVertices[i]].x - touchX * (speed * Time.deltaTime),
                        vertices[holeVertices[i]].y + touchY * (speed * Time.deltaTime), vertices[holeVertices[i]].z);

                    mesh.vertices = vertices;

                    holeCenter.position = new Vector3(holeCenter.position.x - touchX * ((speed / centerSpeedRatio) * Time.deltaTime),
                    holeCenter.position.y, holeCenter.position.z - touchY * ((speed / centerSpeedRatio) * Time.deltaTime));
                }
            }
        }

        private void NonMobileControl()
        {
            if (Input.GetMouseButton(0))
            {
                touchX = Input.GetAxis("Mouse X");
                touchY = Input.GetAxis("Mouse Y");

                //holeCenter.position = new Vector3(
                //    Mathf.Clamp(holeCenter.position.x, maxLimits.x, minLimits.x),
                //    holeCenter.position.y,
                //    Mathf.Clamp(holeCenter.position.z, minLimits.z, maxLimits.z));

                for (int i = 0; i < holeVertices.Count; i++)
                {
                    Vector3[] vertices = mesh.vertices;
                    vertices[holeVertices[i]] = new Vector3(vertices[holeVertices[i]].x - touchX * (speed * Time.deltaTime),
                        vertices[holeVertices[i]].y + touchY * (speed * Time.deltaTime), vertices[holeVertices[i]].z);

                    mesh.vertices = vertices;

                    holeCenter.position = new Vector3(holeCenter.position.x - touchX * ((speed / centerSpeedRatio) * Time.deltaTime),
                        holeCenter.position.y, holeCenter.position.z - touchY * ((speed / centerSpeedRatio) * Time.deltaTime));
                }
            }
        }

        private void CheckLimits()
        {

        }

        private void UpdateHoleVerticesPosition()
        {
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;
        }

        private void GetHoleVertices()
        {
            for(int i=0; i<mesh.vertices.Length; i++)
            {
                currentVertexPosition = localToWorld.MultiplyPoint3x4(mesh.vertices[i]);

                if (Vector3.Distance(holeCenter.position, currentVertexPosition) < radius)
                {
                    holeVertices.Add(i);
             //       offsets.Add(currentVertexPosition - holeCenter.position);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(holeCenter.position, radius);
        }
    }
}