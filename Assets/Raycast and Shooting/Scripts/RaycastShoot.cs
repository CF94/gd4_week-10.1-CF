using UnityEngine;
using System.Collections;
using Mono.Cecil.Cil;

public class RaycastShoot : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;

    public Transform gunEnd;

    private Camera fpsCam;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.15f);

    private AudioSource gunAudio;

    private LineRenderer laserLine;

    private float nextFire;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }
    void Update()
    {
        laserLine.SetPosition(0, gunEnd.position);

        if (Input.GetKeyDown (KeyCode.Mouse0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

            RaycastHit hit;


            // Determine point of origin for raycast, direction of raycast, returning what is hit as a value, and preventing anything from being hit outside of the range            
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                //targets to be shot
                ShootableBox health = hit.collider.GetComponent<ShootableBox>();                

                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }

                EnemyController ragdollSwitcher = hit.collider.GetComponent<EnemyController>();

                if (ragdollSwitcher != null)
                {
                    ragdollSwitcher.ragdollTrigger();
                }
            }

            else
            {
               laserLine.SetPosition(1, fpsCam.transform.forward * 100000);
            }
        }
    }
    private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;        
    }
}
