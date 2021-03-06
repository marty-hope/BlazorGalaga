﻿using System;
using System.Collections.Generic;
using System.Drawing;
using BlazorGalaga.Interfaces;
using BlazorGalaga.Models.Paths.Intros;
using BlazorGalaga.Static;

namespace BlazorGalaga.Models.Paths.Challenges.Challenge3
{
    public class Challenge1 : IntroBase
    {
        public Challenge1()
        {
            IsChallenge = true;
            IntroLocation = IntroLocation.Top;
        }

        public override List<BezierCurve> GetPaths()
        {
            List<BezierCurve> paths = new List<BezierCurve>
            {
                new BezierCurve() {StartPoint = new PointF(281.0104F,1F),
                ControlPoint1 = new PointF(285.7423F,130.1253F),
                ControlPoint2 = new PointF(340.1589F,739.3484F),
                EndPoint = new PointF(3.012873F,671.9198F)},

                new BezierCurve() {StartPoint = new PointF(12.47662F,670.7368F),
                ControlPoint1 = new PointF(337.7929F,738.1654F),
                ControlPoint2 = new PointF(288.1083F,124.2105F),
                EndPoint = new PointF(282.1934F,-50F)},
            };

            return paths;
        }
    }
}
