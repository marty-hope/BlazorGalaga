﻿using System;
using System.Collections.Generic;
using System.Drawing;
using BlazorGalaga.Interfaces;
using BlazorGalaga.Models;
using BlazorGalaga.Models.Paths;
using BlazorGalaga.Models.Paths.Challenges;
using BlazorGalaga.Services;

namespace BlazorGalaga.Static.Levels
{
    public static class Level2
    {
        public static void InitIntro(AnimationService animationService)
        {
            //two groups of four from top
            for (int i = 0; i < 4; i++)
                animationService.Animatables.Add(BugFactory.CreateAnimatable_BugIntro(i, i * Constants.BugIntroSpacing, new Intro1(), Sprite.SpriteTypes.BlueBug));
            for (int i = 0; i < 4; i++)
                animationService.Animatables.Add(BugFactory.CreateAnimatable_BugIntro(i + 4, i * Constants.BugIntroSpacing, new Intro2(), Sprite.SpriteTypes.RedBug));

            //two groups of stacked eight from bottom
            for (int i = 1; i < 8; i += 2)
                animationService.Animatables.Add(BugFactory.CreateAnimatable_BugIntro(i + 8, (i * Constants.BugIntroSpacing) / 2, new Intro3(), Sprite.SpriteTypes.GreenBug));

            for (int i = 0; i < 8; i += 2)
                animationService.Animatables.Add(BugFactory.CreateAnimatable_BugIntro(i + 8, (i * Constants.BugIntroSpacing) / 2, new Intro5(), Sprite.SpriteTypes.RedBug));

            for (int i = 0; i < 4; i++)
                animationService.Animatables.Add(BugFactory.CreateAnimatable_BugIntro(i + 16, i * Constants.BugIntroSpacing, new Intro4(), Sprite.SpriteTypes.RedBug));

            for (int i = 0; i < 4; i++)
                animationService.Animatables.Add(BugFactory.CreateAnimatable_BugIntro(i + 20, i * Constants.BugIntroSpacing, new Intro6(), Sprite.SpriteTypes.RedBug));

            //two groups of stacked eight from top
            for (int i = 0; i < 4; i++)
                animationService.Animatables.Add(BugFactory.CreateAnimatable_BugIntro(i + 24, i * Constants.BugIntroSpacing, new Intro2(), Sprite.SpriteTypes.BlueBug));

            for (int i = 0; i < 4; i++)
                animationService.Animatables.Add(BugFactory.CreateAnimatable_BugIntro(i + 28, i * Constants.BugIntroSpacing, new Intro7(), Sprite.SpriteTypes.BlueBug));

            for (int i = 0; i < 4; i++)
                animationService.Animatables.Add(BugFactory.CreateAnimatable_BugIntro(i + 32, i * Constants.BugIntroSpacing, new Intro1(), Sprite.SpriteTypes.BlueBug));

            for (int i = 0; i < 4; i++)
                animationService.Animatables.Add(BugFactory.CreateAnimatable_BugIntro(i + 36, i * Constants.BugIntroSpacing, new Intro8(), Sprite.SpriteTypes.BlueBug));
            
        }
    }
}