/*
"Disofenix 117"
Diego Esteban Suarez C.		1201689
Universidad Militar Nueva Granada
-   -   SIMLACION   -   -
2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
struct vector
{
    float x;
    float y;
    float z;
}

public class SimulationEngine : MonoBehaviour
{ 
    
    public Vector3 m0Position0;          //posicion inicial
    public Vector3 m0Velocity0;          //velocidad inicial


    public Vector3 m1Position0;          //posicion inicial
    public Vector3 m1Velocity0;          //velocidad inicial

    public Vector3 m2Position0;          //posicion inicial
    public Vector3 m2Velocity0;          //velocidad inicial


    public float mstep;                 //paso del metodo h

    private GameObject mView0 = null;    //esfera para visualizar el movimiento de la particula
    private GameObject mView1 = null;    //esfera para visualizar el movimiento de la particula
    private GameObject mView2 = null;    //esfera para visualizar el movimiento de la particula

    private Vector3 m0Position;          //posicion numerico de la particula
    private Vector3 m0Velocity;          //velocidad numerico de la particula


    private Vector3 m1Position;          //posicion numerico de la particula
    private Vector3 m1Velocity;          //velocidad numerico de la particula

    private Vector3 m2Position;          //posicion numerico de la particula
    private Vector3 m2Velocity;          //velocidad numerico de la particula

    public Vector3 Fuerza1;             //fuerza aplicada a la particula
    public Vector3 Fuerza2;             //fuerza aplicada a la particula
    public Vector3 FuerzaT;             //fuerza aplicada a la particula

    private Vector3 m1Force;             //fuerza aplicada a la particula
    private Vector3 m2Force;             //fuerza aplicada a la particula


    private float mTime;                //tiempo de la simulacion
    private float m0Mass;                //masa de la masa
    private float m1Mass;                //masa de la masa
    private float m2Mass;                //masa de la particula

    private float G = 6.67408e-11f;     //constante universal gravitatoria
    private float G1, G2;
    private float r1,r2;

    private float s1;                   //friccion 1



    // Start is called before the first frame update
    void Start()
    {
        mTime = 0.0f;

        mstep = 0.0005f;

        m0Mass = 1000.0f;
        m1Mass = m0Mass;
        m2Mass = 1.0f;

        G = 1.0f;
        Fuerza1 = new Vector3();
        Fuerza2 = new Vector3();
        FuerzaT = new Vector3();


        //Masa 1
        m0Position0 = new Vector3(-50.0f, 10.0f, 0.0f);
        m0Position = m0Position0;

        //Masa 2
        m1Position0 = new Vector3(50.0f, 10.0f, 0.0f);
        m1Position = m1Position0;

        //Particula
        m2Position0 = new Vector3(0.0f, 10.0f, 50.0f);
        m2Position = m2Position0;

        m2Velocity0 = new Vector3(0.0f, 0.0f, 0.0f);
        m2Velocity = m2Velocity0;

        mView0 = GameObject.Find("Sphere");
        mView1 = GameObject.Find("Sphere1");
        mView2 = GameObject.Find("Sphere2");
    }
    private void CalcularVariablesInstantaneas()
    {

        r1 = Vector3.Magnitude(m2Position-m0Position);
        r2= Vector3.Magnitude(m2Position - m1Position);

        G1 = G*(m2Mass * m0Mass) / (r1 * r1);
        G2 = G * (m2Mass * m1Mass) / (r2 * r2);

        Fuerza1 += -G1 * Vector3.Normalize(m2Position-m0Position);
        Fuerza2 += -G2 * Vector3.Normalize(m2Position-m1Position);
        FuerzaT = Fuerza1 + Fuerza2;

    }
    private void EcGenMov()
    {
        m2Position += m2Position * mstep;
        m2Velocity += (FuerzaT / m2Mass) * mstep;
    }
    private void MetodoEuler()//metodo euler
    {

        m2Position += (m2Velocity * mstep);
        m2Velocity += (FuerzaT / m2Mass) * mstep;


    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("Tiempo " + mTime);

        CalcularVariablesInstantaneas();

        //EcGenMov();
        MetodoEuler();

        mView0.transform.position = m0Position;
        mView1.transform.position = m1Position;
        mView2.transform.position = m2Position;

        mTime += mstep;

    }
}
