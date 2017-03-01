// from: http://www.robertpenner.com/easing/

// Removed animation stuff - not needed for Unity.
// Fixed documentation erroneously calling param "c" Final value. If c == 0 then fuck all happens. Therefore:
// c is not the final value and the person who wrote that it is, happens to be an asshole that wasted me an hour
// figuring out why.

using System;

namespace com.AmazingFusion {   

    /// <summary>
    /// Animates the change in value of a double property using 
    /// Robert Penner's easing equations for interpolation over a specified duration.
    /// </summary>
    public static class EasingCurves
    {
        public enum Curve
        {
            Linear,
            ExpoEaseOut,
            ExpoEaseIn,
            ExpoEaseInOut,
            ExpoEaseOutIn,
            CircEaseOut,
            CircEaseIn,
            CircEaseInOut,
            CircEaseOutIn,
            QuadEaseOut,
            QuadEaseIn,
            QuadEaseInOut,
            QuadEaseOutIn,
            SineEaseOut,
            SineEaseIn,
            SineEaseInOut,
            SineEaseOutIn,
            CubicEaseOut,
            CubicEaseIn,
            CubicEaseInOut,
            CubicEaseOutIn,
            QuartEaseOut,
            QuartEaseIn,
            QuartEaseInOut,
            QuartEaseOutIn,
            QuintEaseOut,
            QuintEaseIn,
            QuintEaseInOut,
            QuintEaseOutIn,
            ElasticEaseOut,
            ElasticEaseIn,
            ElasticEaseInOut,
            ElasticEaseOutIn,
            BounceEaseOut,
            BounceEaseIn,
            BounceEaseInOut,
            BounceEaseOutIn,
            BackEaseOut,
            BackEaseIn,
            BackEaseInOut,
            BackEaseOutIn
        }

        /// <summary>
        /// Evaluates a defined easing curve.
        /// Useful when assigning delegates to control animation. 
        /// </summary>
        /// <param name="time">Current time in seconds.</param>
        /// <param name="startingValue">Starting value.</param>
        /// <param name="change">Change in value.</param>
        /// <param name="duration">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double Evaluate(Curve curve, double time, double startingValue, double change, double duration)
        {
            switch (curve)
            {
                case Curve.Linear:
                    return Linear(time, startingValue, change, duration);

                case Curve.ExpoEaseOut:
                    return ExpoEaseOut(time, startingValue, change, duration);

                case Curve.ExpoEaseIn:
                    return ExpoEaseIn(time, startingValue, change, duration);

                case Curve.ExpoEaseInOut:
                    return ExpoEaseInOut(time, startingValue, change, duration);

                case Curve.ExpoEaseOutIn:
                    return ExpoEaseOutIn(time, startingValue, change, duration);

                case Curve.CircEaseOut:
                    return CircEaseOut(time, startingValue, change, duration);

                case Curve.CircEaseIn:
                    return CircEaseIn(time, startingValue, change, duration);

                case Curve.CircEaseInOut:
                    return CircEaseInOut(time, startingValue, change, duration);

                case Curve.CircEaseOutIn:
                    return CircEaseOutIn(time, startingValue, change, duration);

                case Curve.QuadEaseOut:
                    return QuadEaseOut(time, startingValue, change, duration);

                case Curve.QuadEaseIn:
                    return QuadEaseIn(time, startingValue, change, duration);

                case Curve.QuadEaseInOut:
                    return QuadEaseInOut(time, startingValue, change, duration);

                case Curve.QuadEaseOutIn:
                    return QuadEaseOutIn(time, startingValue, change, duration);

                case Curve.SineEaseOut:
                    return SineEaseOut(time, startingValue, change, duration);

                case Curve.SineEaseIn:
                    return SineEaseIn(time, startingValue, change, duration);

                case Curve.SineEaseInOut:
                    return SineEaseInOut(time, startingValue, change, duration);

                case Curve.SineEaseOutIn:
                    return SineEaseOutIn(time, startingValue, change, duration);

                case Curve.CubicEaseOut:
                    return CubicEaseOut(time, startingValue, change, duration);

                case Curve.CubicEaseIn:
                    return CubicEaseIn(time, startingValue, change, duration);

                case Curve.CubicEaseInOut:
                    return CubicEaseInOut(time, startingValue, change, duration);

                case Curve.CubicEaseOutIn:
                    return CubicEaseOutIn(time, startingValue, change, duration);

                case Curve.QuartEaseOut:
                    return QuartEaseOut(time, startingValue, change, duration);

                case Curve.QuartEaseIn:
                    return QuartEaseIn(time, startingValue, change, duration);

                case Curve.QuartEaseInOut:
                    return QuartEaseInOut(time, startingValue, change, duration);

                case Curve.QuartEaseOutIn:
                    return QuartEaseOutIn(time, startingValue, change, duration);

                case Curve.QuintEaseOut:
                    return QuintEaseOut(time, startingValue, change, duration);

                case Curve.QuintEaseIn:
                    return QuintEaseIn(time, startingValue, change, duration);

                case Curve.QuintEaseInOut:
                    return QuintEaseInOut(time, startingValue, change, duration);

                case Curve.QuintEaseOutIn:
                    return QuintEaseOutIn(time, startingValue, change, duration);

                case Curve.ElasticEaseOut:
                    return ElasticEaseOut(time, startingValue, change, duration);

                case Curve.ElasticEaseIn:
                    return ElasticEaseIn(time, startingValue, change, duration);

                case Curve.ElasticEaseInOut:
                    return ElasticEaseInOut(time, startingValue, change, duration);

                case Curve.ElasticEaseOutIn:
                    return ElasticEaseOutIn(time, startingValue, change, duration);

                case Curve.BounceEaseOut:
                    return BounceEaseOut(time, startingValue, change, duration);

                case Curve.BounceEaseIn:
                    return BounceEaseIn(time, startingValue, change, duration);

                case Curve.BounceEaseInOut:
                    return BounceEaseInOut(time, startingValue, change, duration);

                case Curve.BounceEaseOutIn:
                    return BounceEaseOutIn(time, startingValue, change, duration);

                case Curve.BackEaseOut:
                    return BackEaseOut(time, startingValue, change, duration);

                case Curve.BackEaseIn:
                    return BackEaseIn(time, startingValue, change, duration);

                case Curve.BackEaseInOut:
                    return BackEaseInOut(time, startingValue, change, duration);

                case Curve.BackEaseOutIn:
                    return BackEaseOutIn(time, startingValue, change, duration);

            }

            return 0;
        }


        /// <summary>
        /// Linear change in value.
        /// Useful when assigning delegates to control animation. 
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double Linear(double t, double b, double c, double d)
        {
            return c * t / d + b;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double ExpoEaseOut(double t, double b, double c, double d)
        {
            return (t == d) ? b + c : c * (-Math.Pow(2, -10 * t / d) + 1) + b;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double ExpoEaseIn(double t, double b, double c, double d)
        {
            return (t == 0) ? b : c * Math.Pow(2, 10 * (t / d - 1)) + b;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double ExpoEaseInOut(double t, double b, double c, double d)
        {
            if (t == 0)
                return b;

            if (t == d)
                return b + c;

            if ((t /= d / 2) < 1)
                return c / 2 * Math.Pow(2, 10 * (t - 1)) + b;

            return c / 2 * (-Math.Pow(2, -10 * --t) + 2) + b;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double ExpoEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return ExpoEaseOut(t * 2, b, c / 2, d);

            return ExpoEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double CircEaseOut(double t, double b, double c, double d)
        {
            return c * Math.Sqrt(1 - (t = t / d - 1) * t) + b;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double CircEaseIn(double t, double b, double c, double d)
        {
            return -c * (Math.Sqrt(1 - (t /= d) * t) - 1) + b;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double CircEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return -c / 2 * (Math.Sqrt(1 - t * t) - 1) + b;

            return c / 2 * (Math.Sqrt(1 - (t -= 2) * t) + 1) + b;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double CircEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return CircEaseOut(t * 2, b, c / 2, d);

            return CircEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuadEaseOut(double t, double b, double c, double d)
        {
            return -c * (t /= d) * (t - 2) + b;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuadEaseIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t + b;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuadEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t + b;

            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuadEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return QuadEaseOut(t * 2, b, c / 2, d);

            return QuadEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double SineEaseOut(double t, double b, double c, double d)
        {
            return c * Math.Sin(t / d * (Math.PI / 2)) + b;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double SineEaseIn(double t, double b, double c, double d)
        {
            return -c * Math.Cos(t / d * (Math.PI / 2)) + c + b;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double SineEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * (Math.Sin(Math.PI * t / 2)) + b;

            return -c / 2 * (Math.Cos(Math.PI * --t / 2) - 2) + b;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double SineEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return SineEaseOut(t * 2, b, c / 2, d);

            return SineEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double CubicEaseOut(double t, double b, double c, double d)
        {
            return c * ((t = t / d - 1) * t * t + 1) + b;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double CubicEaseIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t + b;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double CubicEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t + b;

            return c / 2 * ((t -= 2) * t * t + 2) + b;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double CubicEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return CubicEaseOut(t * 2, b, c / 2, d);

            return CubicEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuartEaseOut(double t, double b, double c, double d)
        {
            return -c * ((t = t / d - 1) * t * t * t - 1) + b;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuartEaseIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t * t + b;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuartEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t * t + b;

            return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuartEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return QuartEaseOut(t * 2, b, c / 2, d);

            return QuartEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuintEaseOut(double t, double b, double c, double d)
        {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuintEaseIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t * t * t + b;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuintEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double QuintEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return QuintEaseOut(t * 2, b, c / 2, d);
            return QuintEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double ElasticEaseOut(double t, double b, double c, double d)
        {
            if ((t /= d) == 1)
                return b + c;

            double p = d * .3;
            double s = p / 4;

            return (c * Math.Pow(2, -10 * t) * Math.Sin((t * d - s) * (2 * Math.PI) / p) + c + b);
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double ElasticEaseIn(double t, double b, double c, double d)
        {
            if ((t /= d) == 1)
                return b + c;

            double p = d * .3;
            double s = p / 4;

            return -(c * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b;
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double ElasticEaseInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) == 2)
                return b + c;

            double p = d * (.3 * 1.5);
            double s = p / 4;

            if (t < 1)
                return -.5 * (c * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b;
            return c * Math.Pow(2, -10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p) * .5 + c + b;
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double ElasticEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return ElasticEaseOut(t * 2, b, c / 2, d);
            return ElasticEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double BounceEaseOut(double t, double b, double c, double d)
        {
            if ((t /= d) < (1 / 2.75))
                return c * (7.5625 * t * t) + b;
            else if (t < (2 / 2.75))
                return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b;
            else if (t < (2.5 / 2.75))
                return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b;
            else
                return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double BounceEaseIn(double t, double b, double c, double d)
        {
            return c - BounceEaseOut(d - t, 0, c, d) + b;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double BounceEaseInOut(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return BounceEaseIn(t * 2, 0, c, d) * .5 + b;
            else
                return BounceEaseOut(t * 2 - d, 0, c, d) * .5 + c * .5 + b;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double BounceEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return BounceEaseOut(t * 2, b, c / 2, d);
            return BounceEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double BackEaseOut(double t, double b, double c, double d)
        {
            return c * ((t = t / d - 1) * t * ((1.70158 + 1) * t + 1.70158) + 1) + b;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double BackEaseIn(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * ((1.70158 + 1) * t - 1.70158) + b;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double BackEaseInOut(double t, double b, double c, double d)
        {
            double s = 1.70158;
            if ((t /= d / 2) < 1)
                return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double BackEaseOutIn(double t, double b, double c, double d)
        {
            if (t < d / 2)
                return BackEaseOut(t * 2, b, c / 2, d);
            return BackEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
    }

}