﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGalaga.Models;
using BlazorGalaga.Static;
using System.Linq;
using BlazorGalaga.Interfaces;
using System.Text.Json;

namespace BlazorGalaga.Services
{
    public class AnimationService
    {
        public List<IAnimatable> Animatables = new List<IAnimatable>();

        private BezierCurveService bezierCurveService;
        private SpriteService spriteService;
        private long AnimationCount = 0;

        public AnimationService(BezierCurveService bcs, SpriteService ss)
        {
            bezierCurveService = bcs;
            spriteService = ss;
        }

        public void ResetCanvas(Canvas2DContext ctx)
        {
            ctx.ClearRectAsync(0, 0, Constants.CanvasSize.Width, Constants.CanvasSize.Height);
        }

        public void Animate()
        {
            foreach (IAnimatable animatable in Animatables.Where(a=>a.Started))
            {
                if (animatable.StartDelay > 0 && !animatable.StartDelayStarted)
                {
                    animatable.CurPathPointIndex = animatable.StartDelay;
                    animatable.StartDelayStarted = true;
                }

                try
                {
                    if (animatable.CurPathPointIndex - 1 > 0 && animatable.CurPathPointIndex - 1 < animatable.PathPoints.Count)
                        animatable.PevLocation = animatable.PathPoints[animatable.CurPathPointIndex - 1];

                    if (animatable.CurPathPointIndex > 0 && animatable.CurPathPointIndex  < animatable.PathPoints.Count)
                        animatable.Location = animatable.PathPoints[animatable.CurPathPointIndex];

                    if (animatable.CurPathPointIndex + 1 > 0 && animatable.CurPathPointIndex + 1 < animatable.PathPoints.Count)
                        animatable.NextLocation = animatable.PathPoints[animatable.CurPathPointIndex + 1];
                }
                catch (Exception ex)
                {
                    Utils.dOut("Animation Error", ex.Message + "<br/>" + ex.StackTrace + "<br/>" + " animatable.CurPathPointIndex: " + animatable.CurPathPointIndex + " animatable.PathPoints.Count: " + animatable.PathPoints.Count);
                }

                animatable.Rotation = bezierCurveService.GetRotationAngleAlongPath(animatable);
                animatable.CurPathPointIndex += animatable.Speed;
                animatable.IsMoving = true;

                if (animatable.CurPathPointIndex > animatable.PathPoints.Count - 1)
                {
                    //this stops the animation
                    animatable.CurPathPointIndex -= animatable.Speed;
                    animatable.CurPathPointIndex = animatable.PathPoints.Count-1;
                    animatable.IsMoving = false;
                    if (animatable.LoopBack) animatable.Speed *= -1;
                }
                if (animatable.CurPathPointIndex < 0)
                {
                    //this stops the animation
                    animatable.CurPathPointIndex = 0;
                    animatable.CurPathPointIndex = animatable.PathPoints.Count - 1;
                    animatable.IsMoving = false;
                    if (animatable.LoopBack) animatable.Speed *= -1;
                }
            }

        }

        public void ComputePathPoints()
        {
            float pointgranularity = 1F; //the lower the more granular 
    
            foreach (var animatable in Animatables)
            {
                if (animatable.Paths != null)
                {
                    animatable.PathPoints = new List<PointF>();
                    foreach (BezierCurve path in animatable.Paths)
                    {
                        for (var percent = 0F; percent <= 100; percent+= .01F)
                        {
                            PointF point;
                            if (animatable.PathIsLine)
                                point = bezierCurveService.getLineXYatPercent(path, percent);
                            else
                                point = bezierCurveService.getCubicBezierXYatPercent(path, percent);
                            animatable.PathPoints.Add(point);
                        }
                    }
                    animatable.PathPoints = bezierCurveService.GetEvenlyDistributedPathPointsByLength(animatable.PathPoints, pointgranularity);
                    var json = JsonSerializer.Serialize(animatable.PathPoints);
                    Console.WriteLine(json);
                }
            }
        }

        public void Draw()
        {
            AnimationCount += 1;

            spriteService.DynamicCtx.BeginBatchAsync();

            ResetCanvas(spriteService.DynamicCtx);

            foreach (IAnimatable animatable in Animatables.Where(a=>a.Started)) {

                spriteService.DrawSprite(
                    animatable.Sprite,
                    animatable.Location,
                    (animatable.RotateAlongPath && animatable.IsMoving) ? animatable.Rotation : 0
                    );

                //bezierCurveService.DrawGrid(spriteService.CanvasCtx);

                foreach (BezierCurve path in animatable.Paths)
                {
                    if (animatable.DrawPath)
                        bezierCurveService.DrawCurve(spriteService.DynamicCtx, path);
                    if (animatable.DrawControlLines)
                        bezierCurveService.DrawCurveControlLines(spriteService.DynamicCtx, path);
                }
            }

            spriteService.DynamicCtx.EndBatchAsync();
        }
    }
}
