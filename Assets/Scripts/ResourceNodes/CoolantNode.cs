﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Consumes: Power
/// Generates: Heat (decrease)
/// </summary>
public class CoolantNode : MonoBehaviour {
  public float coolantHeatRate = -0.6f;

  public ParticleSystem coolantParticles = null;

  // Cached component references
  private ResourceStore m_resourceStore = null;
  private ClickableNode m_clickableNode = null;

  //Audio SFX
  private AudioSource optionalAudio;

  void Awake()
  {
    optionalAudio = gameObject.GetComponent<AudioSource>() as AudioSource;
    if (optionalAudio != null)
    {
      optionalAudio.Stop();
    }
  }

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {
    m_resourceStore = GameObject.FindObjectOfType<ResourceStore>();
    if (!m_resourceStore) {
      Debug.LogError("Could not find ResourceStore");
    }
    m_clickableNode = GetComponent<ClickableNode>();
    if (!m_clickableNode) {
      Debug.LogError("Could not find ClickableNode");
    }

    if (!coolantParticles) {
      Debug.LogError("coolantParticles not set!");
    }
    coolantParticles.Stop();
  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {
    if (isActiveAndEnabled) {
      // Apply node update if active
      if (m_clickableNode.isNodeActive) {
        m_resourceStore.ChangeResourceValue(EResource.Heat, coolantHeatRate * Time.deltaTime);

        if (!coolantParticles.isPlaying) {
          coolantParticles.Play();
          if (optionalAudio != null)
          {
            optionalAudio.Play();
          }
        }
      } else {
        if (coolantParticles.isPlaying) {
          coolantParticles.Stop();
          if (optionalAudio != null)
          {
            optionalAudio.Stop();
          }
        }
      }
    }
  }
}
