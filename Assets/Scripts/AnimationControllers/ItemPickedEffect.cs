﻿using Models;
using Models.Items;
using UnityEngine;

namespace AnimationControllers
{
    public class ItemPickedEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem itemPickedEffect;
        [SerializeField] private Color keyColor;
        [SerializeField] private Color lifeSaverColor;
        [SerializeField] private Color boosterColor;

        private void OnEnable()
        {
            Key.OnPicked += KeyPickedEffect;
            Lock.OnUnlocked += KeyPickedEffect;
            LifeSaver.OnPicked += LifeSaverPickedEffect;
            Booster.OnPicked += BoosterPickedEffect;
        }
        
        private void OnDisable()
        {
            Key.OnPicked -= KeyPickedEffect;
            Lock.OnUnlocked -= KeyPickedEffect;
            LifeSaver.OnPicked -= LifeSaverPickedEffect;
            Booster.OnPicked -= BoosterPickedEffect;
        }

        private void KeyPickedEffect()
        {
            itemPickedEffect.transform.localPosition = Player.Position;
            var main = itemPickedEffect.main;
            main.startColor = new ParticleSystem.MinMaxGradient(keyColor);
            itemPickedEffect.Play();
        }
        
        private void LifeSaverPickedEffect()
        {
            itemPickedEffect.transform.localPosition = Player.Position;
            var main = itemPickedEffect.main;
            main.startColor = new ParticleSystem.MinMaxGradient(lifeSaverColor);
            itemPickedEffect.Play();
        }
        
        private void BoosterPickedEffect()
        {
            itemPickedEffect.transform.localPosition = Player.Position;
            var main = itemPickedEffect.main;
            main.startColor = new ParticleSystem.MinMaxGradient(boosterColor);
            itemPickedEffect.Play();
        }
    }
}