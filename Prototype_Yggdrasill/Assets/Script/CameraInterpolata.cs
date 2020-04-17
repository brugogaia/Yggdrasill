using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraInterpolata : MonoBehaviour
{
    private const int FOV_PICCOLO = 60;
    private const int FOV_GRANDE = 75;
    private GameObject target_tmp;
    private Vector3 punto_a_inseguimento;

    private float velocita_verticale_camera = 1.0f;
    private float velocita_orizzontale_camera = 1.0f;

    [SerializeField]
    private GameObject giocatore;
    private const float maxDistanzaCamera = 5.0f;
    private const float VEL_ROT = 5f;
    private const float VEL_MOV = 5f;
    private const float RAGGIO_CERCHIO_CAMERA = 3;

    //L' angolo minimo tra -g e il punto a inseguimento
    private const float ANGOLO_VERTICALE_MINIMO_CAMERA = 45.0f;


    //L' angolo massimo tra -g e il punto a inseguimento in cui si inizia
    // a ruotare la camera artificialmente in alto
    private const float ANGOLO_VERTICALE_MINIMO_CAMERA_IN_ALTO = 90.0f + 10.0f;



    //L' angolo massimo tra -g e il punto a inseguimento
    private const float ANGOLO_VERTICALE_MASSIMO_CAMERA = 90.0f + 30.0f;

    private void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);
        // DontDestroyOnLoad(giocatore);
    }

    public void DeathImmobility()
    {
        giocatore = null;
        velocita_orizzontale_camera = 0f;
        velocita_verticale_camera = 0f;
    }

    void Start()
    {
        giocatore = GameObject.FindGameObjectWithTag("Player");


        Vector3 puntoDietro = new Vector3(-1, 0.7f, 0).normalized;
        punto_a_inseguimento = puntoDietro * RAGGIO_CERCHIO_CAMERA + giocatore.transform.position;
        this.transform.position = punto_a_inseguimento;



        target_tmp = new GameObject();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private Vector3 appiana_target_libero(Vector3 punto_target_camera_inseguimento)
    {
        Vector3 a_normalizzato = (punto_target_camera_inseguimento - giocatore.transform.position).normalized;
        Vector3 meno_g = -Physics.gravity.normalized;


        //Angolo con versore -g
        float angolo_versore_g = Mathf.Acos(
            Vector3.Dot(
                a_normalizzato,
                meno_g
            )
        );
        angolo_versore_g = angolo_versore_g / 3.14f * 180.0f;
        //Debug.Log(angolo_versore_g);
        //return punto_target_camera_inseguimento;
        //Se è già nell'arco giusto
        if (angolo_versore_g > ANGOLO_VERTICALE_MINIMO_CAMERA && angolo_versore_g < ANGOLO_VERTICALE_MASSIMO_CAMERA)
        {
            return punto_target_camera_inseguimento;
        }

        //Altrimenti la ruoto
        float nuovo_angolo = Mathf.Clamp(angolo_versore_g, ANGOLO_VERTICALE_MINIMO_CAMERA, ANGOLO_VERTICALE_MASSIMO_CAMERA);

        target_tmp.transform.position = punto_target_camera_inseguimento;
        target_tmp.transform.LookAt(this.giocatore.transform, -Physics.gravity);
        target_tmp.transform.RotateAround(
            this.giocatore.transform.position,
            this.transform.right,
            -(nuovo_angolo - angolo_versore_g)
        );

        return target_tmp.transform.position;
    }

    private Vector3 ruotaAttornoAlGiocatore(Vector3 posizione_da_ruotare)
    {
        Vector2 mouseMovement = new Vector2();
        mouseMovement.x = Input.GetAxis("Mouse X");
        mouseMovement.y = Input.GetAxis("Mouse Y");

        float angolo_orizzontale_attorno_al_giocatore = 0.0f;
        float angolo_verticale_attorno_al_giocatore = 0.0f;
        if (mouseMovement.magnitude > 0.05)
        {
            //Rotazione asse verticale camera
            angolo_verticale_attorno_al_giocatore -= mouseMovement.y * this.velocita_verticale_camera;

            //Rotazione asse orizzontale camera
            angolo_orizzontale_attorno_al_giocatore = mouseMovement.x * velocita_orizzontale_camera;
        }

        Transform t = target_tmp.transform;
        t.position = posizione_da_ruotare;


        t.LookAt(this.giocatore.transform, -Physics.gravity);

        t.RotateAround(this.giocatore.transform.position, -Physics.gravity, angolo_orizzontale_attorno_al_giocatore);
        t.RotateAround(this.giocatore.transform.position,
            Vector3.Cross(
                Physics.gravity,
                t.position - giocatore.transform.position
            ),
            angolo_verticale_attorno_al_giocatore
        );

        return t.position;
    }

    private Vector3 nuovo_punto_inseguimento()
    {
        float distanza_giocatore_obiettivo = Vector3.Distance(punto_a_inseguimento, giocatore.transform.position);
        //Debug.Log("Distanza giocatore obiettivo: "+distanza_giocatore_obiettivo);

        float limite_superiore = RAGGIO_CERCHIO_CAMERA + RAGGIO_CERCHIO_CAMERA * 0.2f;
        float limite_inferiore = RAGGIO_CERCHIO_CAMERA - RAGGIO_CERCHIO_CAMERA * 0.2f;

        if (distanza_giocatore_obiettivo >= limite_inferiore && distanza_giocatore_obiettivo <= limite_superiore)
        {
            return punto_a_inseguimento;
        }


        Vector3 tmp;
        if (distanza_giocatore_obiettivo > limite_superiore)
        {
            tmp = Vector3.Lerp(
                punto_a_inseguimento,
                this.giocatore.transform.position + RAGGIO_CERCHIO_CAMERA * (punto_a_inseguimento - giocatore.transform.position).normalized,
                Time.deltaTime
            );
        }
        else
        {
            tmp = Vector3.Lerp(
                punto_a_inseguimento,
                this.giocatore.transform.position + RAGGIO_CERCHIO_CAMERA * (punto_a_inseguimento - giocatore.transform.position).normalized,
                Time.deltaTime * 7
            );
        }

        return tmp;
    }


    // Update is called once per frame
    void Update()
    {

    }




    private Vector3 raggioNuovaPosCamera(Vector3 punto_obiettivo_raggio)
    {
        ///Parametri per cambiare comportamento camera
        //Angolo di apertura dei raggi supplementari
        //Fa da moltiplicatore rispetto all'unità
        float distanza_aggiunta_di_lato = 0.2f;


        Vector3 direzioneCamera = Vector3.Normalize(punto_obiettivo_raggio - this.giocatore.transform.position);
        //

        //lista raggi che vengono creati
        List<Ray> raggi_da_lanciare = new List<Ray>();
        //Centrale
        raggi_da_lanciare.Add(new Ray(
            giocatore.transform.position,
            direzioneCamera
            ));

        //Giù
        raggi_da_lanciare.Add(
            new Ray(
                giocatore.transform.position,
                direzioneCamera + Vector3.Normalize(Physics.gravity) * distanza_aggiunta_di_lato
            )
        );
        //Su
        raggi_da_lanciare.Add(
            new Ray(
                giocatore.transform.position,
                direzioneCamera - Vector3.Normalize(Physics.gravity) * distanza_aggiunta_di_lato
            )
        );
        //Sx vedendo dalla camera
        raggi_da_lanciare.Add(
            new Ray(
                giocatore.transform.position,
                direzioneCamera + Vector3.Normalize(Vector3.Cross(direzioneCamera, -Physics.gravity)) * distanza_aggiunta_di_lato
            )
        );
        //Dx vedendo dalla camera
        raggi_da_lanciare.Add(
            new Ray(
                giocatore.transform.position,
                direzioneCamera - Vector3.Normalize(Vector3.Cross(direzioneCamera, -Physics.gravity)) * distanza_aggiunta_di_lato
            )
        );

        //Vettore degli hit
        List<RaycastHit> lista_info_collisione = new List<RaycastHit>();
        //Itero e lancio raggi
        foreach (Ray raggio in raggi_da_lanciare)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray: raggio,
                    hitInfo: out hit,
                    maxDistance: maxDistanzaCamera,
                    layerMask: Physics.DefaultRaycastLayers,
                    queryTriggerInteraction: QueryTriggerInteraction.Ignore
                )
            )
            {
                lista_info_collisione.Add(hit);
            }
        }
        //Debug.Log(lista_info_collisione.Count);

        //Ho ottenuto punti di collisione raggi con muri

        if (lista_info_collisione.Count > 0)
        {
            //Calcolo media normali
            Vector3 media_normali = new Vector3();
            Vector3 media_punti_collisione = new Vector3();
            float min_distanza = maxDistanzaCamera;
            foreach (RaycastHit colpo in lista_info_collisione)
            {
                media_normali += colpo.normal;
                media_punti_collisione += colpo.point;
                if (colpo.distance < min_distanza)
                {
                    //Trovo il minimo
                    min_distanza = colpo.distance;
                }
            }
            media_normali = media_normali / lista_info_collisione.Count;
            media_punti_collisione = media_punti_collisione / lista_info_collisione.Count;




            //Raggi di debug che mostrano la posizione della collisione rilevata
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Vector3 up = transform.TransformDirection(Vector3.up) * 10;
            Debug.DrawRay(media_punti_collisione, forward, Color.red);
            Debug.DrawRay(media_punti_collisione, up, Color.red);

            if (//Se l'oggetto con cui colliderebbe la camera è oltre una soglia minima di distanza
                Vector3.Distance(
                    media_punti_collisione,
                    this.giocatore.transform.position) > maxDistanzaCamera / 5f
            )
            {
                //Interpolo la distanza della camera muovendola sul vettore tra giocatore e target
                Vector3 puntoVicinoAlGiocatore = maxDistanzaCamera / 5f * direzioneCamera + this.giocatore.transform.position;
                Vector3 puntoLontanoAlGiocatore = maxDistanzaCamera * direzioneCamera + this.giocatore.transform.position;

                Vector3 tmp = Vector3.Lerp(
                    puntoVicinoAlGiocatore,
                    puntoLontanoAlGiocatore,
                    (min_distanza / maxDistanzaCamera) * 0.9f
                    );

                //Se la camera è parecchio vicina al giocatore, alzo il la posizione di un pochino,
                // senza modificare il target di riferimento
                //Debug.DrawRay(posizioneDaRaggiungere, posizioneDaRaggiungere + Vector3.Normalize(hit.normal)*3, Color.magenta);
                //Debug.Log(media_normali);
                return tmp + media_normali;
            }
            return punto_obiettivo_raggio;
        }
        else
        {
            //Sposto camera in lontananza
            return maxDistanzaCamera * direzioneCamera + this.giocatore.transform.position;
        }
    }

   

    private Vector3 targetInnalzatoSopraGiocatore()
    {
        //Rimanda indietro il punto sopra alla testa del giocatore a cui mirare la camera
        Vector3 a_normalizzato = (punto_a_inseguimento - giocatore.transform.position).normalized;
        Vector3 meno_g = -Physics.gravity.normalized;


        //Angolo con versore -g
        float angolo_versore_g = Mathf.Acos(
            Vector3.Dot(
                a_normalizzato,
                meno_g
            )
        );
        angolo_versore_g = angolo_versore_g / 3.14f * 180.0f;

        float innalzamento = (
            angolo_versore_g - ANGOLO_VERTICALE_MINIMO_CAMERA_IN_ALTO
            )
            /
            (
                ANGOLO_VERTICALE_MASSIMO_CAMERA - ANGOLO_VERTICALE_MINIMO_CAMERA_IN_ALTO
            );
        //Dovrebbe essere tra 0 e 1
        innalzamento = Mathf.Clamp(innalzamento, 0, 1);

        return giocatore.transform.position + -Physics.gravity.normalized * innalzamento * 3;
    }

    void LateUpdate()
    {
        if (this.giocatore==null)
        {
            return;
        }


        punto_a_inseguimento = appiana_target_libero(punto_a_inseguimento);


        //Avvicino target_libero se necessario
        //Nei campi della classe perché va mantenuto
        punto_a_inseguimento = this.nuovo_punto_inseguimento();

        /*if (this.giocatore.GetComponent<Giocatore>().staPortandoOggetto())
        {
            //Fov camera aumento
            Camera c = GetComponent<Camera>();
            if (c.fieldOfView == FOV_PICCOLO)
            {
                DOTween.To(
                    () => c.fieldOfView,
                    x => c.fieldOfView = x,
                    FOV_GRANDE, 1
                );
            }

            // Forza punto inseguimento dietro al giocatore
            punto_a_inseguimento = dietroAlGiocatore(punto_a_inseguimento);
        }
        else
        {
            //Se invece
            //non sto portando l'oggetto, ma il fov è ancora grande
            //Fov camera
            Camera c = GetComponent<Camera>();
            if (c.fieldOfView == FOV_GRANDE)
            {
                DOTween.To(
                    () => c.fieldOfView,
                    x => c.fieldOfView = x,
                    FOV_PICCOLO, 1
                );
            }

            //Se non sta portando oggetto, uso comportamento normale
            //Ruoto in base agli input
            punto_a_inseguimento = this.ruotaAttornoAlGiocatore(punto_a_inseguimento);
        }*/


        //Sparo raggi e assegno posizione da raggiungere
        Vector3 posizioneDaRaggiungere = raggioNuovaPosCamera(punto_a_inseguimento);



        //raggioNuovaPosCamera();


        //Vector3 posizioneDaRaggiungere
        //Interpolo per muovere fluidamente la posizione e rotazione della camera fino al punto voluto
        //Tengo in LateUpdate, dopo che gli altri oggetti sono stati spostati
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(
                Vector3.Normalize(
                    targetInnalzatoSopraGiocatore() - posizioneDaRaggiungere
                    ),
                -Physics.gravity),
            VEL_ROT * Time.deltaTime
        );
        transform.position = Vector3.Lerp(transform.position, posizioneDaRaggiungere, VEL_MOV * Time.deltaTime);


        Debug.DrawRay(punto_a_inseguimento, punto_a_inseguimento + new Vector3(1, 0, 0), Color.green);
        Debug.DrawRay(punto_a_inseguimento, punto_a_inseguimento + new Vector3(0, 1, 0), Color.green);



        //raggioNuovaPosCamera();//Aggiorno i calcoli sulla posizione da raggiungere
    }
}
