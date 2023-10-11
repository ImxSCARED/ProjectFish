using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static FishProperties;
using static TrajectoryPredictor;

public class Cannon : MonoBehaviour
{
    public bool Loaded { get; set; }
    public bool Using { get; set; }
    public bool Fishing { get; set; }

    [Header("Cannon Parts")]
    [SerializeField] private Transform basePivot;
    [SerializeField] private Transform barrelPivot;
    [SerializeField] private Transform projectileStart;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform shipPlayerPosition;


    [Header("Other Parts")]
    [SerializeField] private Cannon otherCannon;
    [SerializeField] private CameraControls thisCameraControls;
    [SerializeField] private Inventory playerInventory;
    //[SerializeField] private Transform playerPosition;
    //If we want a graphic to show the cannon is currently loaded
    [SerializeField] private GameObject loadedGraphic;
    [SerializeField] private Transform joyconGraphic;

    [Header("Control Variables")]
    [SerializeField] private float loadSpeed = 3.0f;
    [SerializeField] private float adjustX = 0f;

    [Header("Audio Variables")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioClip loadClip; 

    [Header("Projectile")]
    [SerializeField] GameObject cannonBall;
    [SerializeField] GameObject harpoon;
    [SerializeField, Range(0.0f, 50.0f)] float force;

    GameObject objectToThrow;
    TrajectoryPredictor trajectoryPredictor;

    private Fish skeweredFish;
    private FishOnHarpoon currentHarpoon;
    private bool harpoonHitFish = false;
    private float reloadTimer;
    
    private bool loading = false;
    bool previousReload;

    Projectile projectile;

    [Header("Inputs")]
    [SerializeField] PlayerInput m_playerInput;
    [SerializeField] string whichCannon;





    // Start is called before the first frame update
    void Start()
    {

        Using = false;
        Loaded = true;

        objectToThrow = cannonBall;
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
        projectile = objectToThrow.GetComponent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerInput.actions[whichCannon].WasPressedThisFrame())
            Activate();
        if (Using)
        {
            if (m_playerInput.actions["Fire"].WasPressedThisFrame())
                ThrowObject();
            
            if (m_playerInput.actions["SwitchWeapon"].WasPressedThisFrame())
                FishActivate();
            if (!harpoonHitFish)
            {
                Predict();

                Vector2 rot = thisCameraControls.GetRotation();
                basePivot.localEulerAngles = new Vector3(basePivot.localEulerAngles.x, rot.y, basePivot.localEulerAngles.z);
                barrelPivot.localEulerAngles = new Vector3(rot.x, barrelPivot.localEulerAngles.y, barrelPivot.localEulerAngles.z);
            }
            if (Fishing && currentHarpoon != null)
            {
                PullMinigame();
            }
            
        }
        if(loading == true)
        {
            reloadTimer += Time.deltaTime / loadSpeed;
            if(reloadTimer > 1)
            {
                Load();
            }
        }
    }

    /// <summary>
    /// Changes camera to cannon mode and makes trajectory visible
    /// </summary>
    void Activate()
    {
        if(Using)
        {
            if(Fishing)
            {
                FishActivate();
            }
            thisCameraControls.SetParameters(CameraControls.CameraSetup.DEFAULT);
            thisCameraControls.MatchTransform(shipPlayerPosition, 0, true);

            trajectoryPredictor.SetTrajectoryVisible(false);

            Using = false;
        }
        else
        {
            if(otherCannon.Using)
            {
                otherCannon.Activate();
            }
            thisCameraControls.SetParameters(CameraControls.CameraSetup.CANNON);
            thisCameraControls.MatchTransform(playerPosition, adjustX, true);
            trajectoryPredictor.SetTrajectoryVisible(true);

            Using = true;
        }
        


        
    }

    /// <summary>
    /// Adjusts everything to prepare or disengage from fishing
    /// </summary>
    /// <param name="ctx"></param>
    void FishActivate()
    {
        
        if (Using && Fishing)
        {
            if(currentHarpoon != null)
            {
                if(harpoonHitFish)
                {
                    skeweredFish.InitiateInOriginalPosition();
                    Destroy(skeweredFish.gameObject);
                    skeweredFish = null;
                } 
                Destroy(currentHarpoon.gameObject);
                HarpoonReturned(false);
            }

            Fishing = false;
            Loaded = previousReload;
            reloadTimer = 0;
            objectToThrow = cannonBall;
            
            basePivot.localEulerAngles -= new Vector3(15, 0, 0);
            basePivot.Translate(projectileStart.forward * -0.5f);
            projectile = objectToThrow.GetComponent<Projectile>();
        }
        else
        {
            previousReload = Loaded;
            loading = false;
            Loaded = true;
            reloadTimer = 0;
            Fishing = true;
            objectToThrow = harpoon;

            basePivot.Translate(projectileStart.forward * 0.5f);
            basePivot.localEulerAngles += new Vector3(15, 0, 0);
            projectile = objectToThrow.GetComponent<Projectile>();
        }
    }
    private void Load()
    {
        reloadTimer = 0;
        Loaded = true;
        loading = false;
        loadedGraphic.SetActive(true);
        source.PlayOneShot(loadClip);
        trajectoryPredictor.SetTrajectoryVisible(true);
    }

    void ThrowObject()
    {
        if (Loaded)
        {
            trajectoryPredictor.SetTrajectoryVisible(false);
            Loaded = false;
            source.PlayOneShot(fireClip);
            loadedGraphic.SetActive(false);
            Projectile p = Instantiate(projectile, projectileStart.position, Quaternion.identity);
            p.Spawn(projectileStart.forward, force);

            if (Fishing)
            {
                p.transform.rotation = barrelPivot.rotation;
                currentHarpoon = p.GetComponent<FishOnHarpoon>();
                currentHarpoon.SetIdentifiers(this);
            }
        }
        else
        {
            if(!Fishing)
                loading = true;
        }

    }

    /// <summary>
    /// Allocates all projectile data in real time then return it so the prediction for trajectory works.
    /// </summary>
    /// <returns></returns>
    ProjectileProperties ProjectileData()
    {
        ProjectileProperties properties = new ProjectileProperties();
        Rigidbody r = objectToThrow.GetComponent<Rigidbody>();

        properties.direction = projectileStart.forward;
        properties.initialPosition = projectileStart.position;
        properties.initialSpeed = force;
        properties.mass = r.mass;
        properties.drag = r.drag;

        return properties;
    }

    void Predict()
    {
            trajectoryPredictor.PredictTrajectory(ProjectileData());
    }

    private void PullMinigame()
    {
        if (harpoonHitFish)
        {
            
            currentHarpoon.returning = true;
            switch (skeweredFish.tier)
            {
                case FishTier.SMALL:
                    currentHarpoon.transform.position = Vector3.MoveTowards(currentHarpoon.transform.position, projectileStart.position, Time.deltaTime * 5f);
                    break;

                case FishTier.MEDIUM:
                    joyconGraphic.parent.gameObject.SetActive(true);
                    joyconGraphic.localPosition = new Vector3(0, -50, 0);

                    currentHarpoon.transform.position = Vector3.MoveTowards(currentHarpoon.transform.position, projectileStart.position, Time.deltaTime * (Mathf.Abs(Input.GetAxis("Vertical")) * 5f));
                    break;

                case FishTier.LARGE:
                    joyconGraphic.parent.gameObject.SetActive(true);
                    joyconGraphic.localPosition = new Vector3(Mathf.PingPong(Time.time * 100, 100) - 50, Mathf.Abs(joyconGraphic.localPosition.x) - 50, joyconGraphic.localPosition.z);
                    Vector3 pullRequirements = joyconGraphic.localPosition.normalized;
                    
                    if ((Input.GetAxis("Vertical") < pullRequirements.y + 0.35f && Input.GetAxis("Vertical") > pullRequirements.y - 0.35f) && (Input.GetAxis("Horizontal") < pullRequirements.x + 0.35f && Input.GetAxis("Horizontal") > pullRequirements.x - 0.35f))
                    {
                        currentHarpoon.transform.position = Vector3.MoveTowards(currentHarpoon.transform.position, projectileStart.position, Time.deltaTime * 5);
                    }
                    else
                    {
                        currentHarpoon.transform.position = Vector3.MoveTowards(currentHarpoon.transform.position, projectileStart.position, Time.deltaTime * -1);
                    }
                    break;
            }
            
            
        }
        else
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > 1.5)
            {
                currentHarpoon.returning = true;
                currentHarpoon.rb.Sleep();
                currentHarpoon.rb.isKinematic = true;
                currentHarpoon.transform.position = Vector3.MoveTowards(currentHarpoon.transform.position, projectileStart.position, 25f * Time.deltaTime);
            }
        }
    }

    public void HarpoonReturned(bool fishHit)
    {
        if(fishHit)
        {
            joyconGraphic.parent.gameObject.SetActive(false);
            playerInventory.AddFish(skeweredFish.theFish);
            skeweredFish = null;
            harpoonHitFish = false;
            Load();
            currentHarpoon = null;
        }
        else
        {
            Load();
            harpoonHitFish = false;
            currentHarpoon = null;
        }
        
    }
    public void HarpoonHitFish(Fish fish)
    {
        skeweredFish = fish;
        harpoonHitFish = true;
    }
 
}
