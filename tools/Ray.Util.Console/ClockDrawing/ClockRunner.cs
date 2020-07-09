using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Maths.Factories;
using Ray.Domain.Model;

namespace Ray.Util.Console.ClockDrawing
{
    class ClockRunner
    {
        public static void DrawClock(string outputBitmapFilePath)
        {
            using var canvas = new System.Drawing.Bitmap(50, 50);

            // Canvas is 50 x 50. (REM top-left = 0,0).
            // Centre of clock in centre of canvas @ 25, 25.
            // Let clock radius be 3/8 of canvas width:
            //   r = 50*3/8 = 18.75

            // For our Ray Tracer calculations P(0,0,0) is the
            // centre of the canvas. All calculations will be
            // based off this. So when plotting the final bitmap
            // pixel, add +25 to the x and y.

            // Script:
            // It should be something like this:
            //  - start with hour 1, going up to hour 12.
            //  - rotate (hour * Pi/6) radians around X-(??)axis.
            //  - translate 18.75 points straight up Y-(??)axis.
            //  - plot pixel.

            /****** UPDATE ******
             * Got this working, but worth pointing out that a fair bit of trail and error and changing order and axis etc.
             * So would need to work through this example again, with reference to the Text ad ensure that have a good grasp.
             * Because 12 dots is about as simple as this is going to get!
             *
             * Changes from the script:
             *  - Had to reverse Translate and Rotate. 
             *  - Had to use the Z axis of the calculated Vector as the X axis on the bitmap!
             *
             * But still encouraging - the principles and API are working, even if I still lack comprehension!
             */

            for (int hour = 1; hour <= 12; hour++)
            {
                var rotation = hour * MathF.PI / 6;
                var builder = new MatrixTransformationBuilder();
                builder.Translate(new Vector3(0F, 0F, 18.75F))
                       .RotateY(rotation);
                DrawProjectilePath(builder, canvas);
            }

            canvas.Save(outputBitmapFilePath);
        }

        private static void DrawProjectilePath(IMatrixTransformationBuilder builder, System.Drawing.Bitmap canvas)
        {
            var hourMark = builder.Execute(new Vector4(0F, 0F, 0F, 1F));

            var roundX = Math.Round(hourMark.X + 25F, 0);
            var roundY = Math.Round(hourMark.Z + 25F, 0);

            // WPF color - see the Projectile game for more info.

            var wpfColor = Color.FromScRgb(1.0F, 0.0F, 0.0F, 1.0F);
            canvas.SetPixel((int)roundX, (int)roundY, wpfColor.Simplify());
        }

    }
}
