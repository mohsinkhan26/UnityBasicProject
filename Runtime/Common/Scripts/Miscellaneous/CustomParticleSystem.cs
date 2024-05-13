/*
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 */

using UnityEngine;

namespace MK.Common.Miscellaneous
{
    /// keep the references of all particle systems
    [RequireComponent(typeof(ParticleSystem), typeof(CustomParticleCulling))]
    public sealed class CustomParticleSystem : MonoBehaviour
    {
        public ParticleSystem[] particleSystems;
        public ParticleSystemRenderer[] particleSystemRenders;
        public CustomParticleCulling particleCulling;
        private GameObject gObject;
        public float StopAfter { get; private set; }

        private bool IsParticlesPlaying
        {
            get
            {
                if (particleSystems == null) return false;
                return particleSystems[0].isPlaying;
            }
        }

        private void Awake()
        {
            if (gObject == null) gObject = gameObject;
            Reset();
        }

#if UNITY_EDITOR
        public
#endif
            // runs only in editor automatically, when you apply this script
            void Reset()
        {
            if (particleSystems == null)
            {
                particleSystems = GetComponentsInChildren<ParticleSystem>();
                particleSystemRenders = GetComponentsInChildren<ParticleSystemRenderer>();
                particleCulling = GetComponent<CustomParticleCulling>();
                particleCulling.target = particleSystems[0];
            }
        }

        /// true: play,     false: stop
        public void PlayParticles(bool _play)
        {
            for (int i = particleSystems.Length - 1; i >= 0; --i)
            {
                if (_play)
                {
                    particleSystems[i].Play();
                    continue;
                }

                particleSystems[i].Stop();
            }

            // it's important to enable/disable the particle, otherwise custom culling script may turn it on/off
            gObject.SetActive(_play);
        }

        public void StopParticlesAfter(float _after)
        {
            StopAfter = _after;
            CancelInvoke(nameof(StopParticles));
            Invoke(nameof(StopParticles), _after);
        }

        private void StopParticles()
        {
            PlayParticles(false);
        }

        public void SetStartColor(Color _color, int _indexParticle = 0)
        {
            var mainModule = particleSystems[_indexParticle].main;
            mainModule.startColor = _color;
        }

        public void SetStartColorNested(Color _color)
        {
            for (int i = particleSystems.Length - 1; i >= 0; --i)
            {
                SetStartColor(_color, i);
            }
        }

        public void SetShapeRadius(float _radius)
        {
            for (int i = particleSystems.Length - 1; i >= 0; --i)
            {
                var shape = particleSystems[i].shape;
                shape.radius = _radius;
            }

            particleCulling.cullingRadius = _radius;
        }

        public void SetShapeRadiusMultiplier(float _radiusMultiplier)
        {
            for (int i = particleSystems.Length - 1; i >= 0; --i)
            {
                var shape = particleSystems[i].shape;
                shape.radius *= _radiusMultiplier;
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (enabled && !IsParticlesPlaying) // if particles are not in playing mode, draw sphere on them
            {
                // Draw gizmos to show the not playing.
                Color col = Color.red;

                Gizmos.color = col;
                Gizmos.DrawWireSphere(transform.position, 0.5f);
            }
        }
#endif
    }
}
