using DiceInvader.Base.Models;
using DiceInvaders.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DiceInvaders.AnimationLayer
{

    public static class GameHelper
    {

        /// <summary>
        /// Returns the full name of the image file representing the invader type
        /// </summary>
        /// <param name="invaderType"> Type of enemy</param>
        /// <returns></returns>
        public static string CreateImageName(InvaderType invaderType)
        {
            string filename;
            switch (invaderType)
            {
                case InvaderType.Enemy1:
                    filename = "enemy1";
                    break;
                default:
                    filename = "enemy2";
                    break;
            }

            return filename + ".png";
        }
        /// <summary>
        /// Returns the full name of the image file representing the shot type
        /// </summary>
        /// <param name="shotType">Type of shot</param>
        /// <returns></returns>
        public static string CreateImageName(ShotType shotType)
        {
            string filename;
            switch (shotType)
            {
                case ShotType.Rocket:
                    filename = "rocket";
                    break;
                default:
                    filename = "bomb";
                    break;

            }

            return filename + ".png";
        }


        public static FrameworkElement InvaderControlFactory(Invader invader, double scale)
        {
            string imageName = CreateImageName(invader.InvaderType);
            AnimatedImage invaderControl = new AnimatedImage(imageName);
            invaderControl.Width = invader.Size.Width * scale;
            invaderControl.Height = invader.Size.Height * scale;
            SetCanvasLocation(invaderControl, invader.Location.X, invader.Location.Y, scale);

            return invaderControl;
        }

        internal static FrameworkElement ShotControlFactory(Shot shot, double scale)
        {
            string imageName = CreateImageName(shot.ShotType);
            AnimatedImage shotControl = new AnimatedImage(imageName);

            shotControl.Width = Shot.ShotSize.Width * scale;
            shotControl.Height = Shot.ShotSize.Height * scale;
            SetCanvasLocation(shotControl, shot.Location.X, shot.Location.Y, scale);
            return shotControl;
        }



        internal static FrameworkElement PlayerControlFactory(Player player, double scale)
        {
            var playerControl = new AnimatedImage("player.png");
            playerControl.Width = player.Size.Width * scale;
            playerControl.Height = player.Size.Height * scale;
            SetCanvasLocation(playerControl, player.Location.X, player.Location.Y, scale);
            return playerControl;
        }

        public static void SetCanvasLocation(FrameworkElement control, double x, double y, double scale)
        {
            Canvas.SetLeft(control, x * scale);
            Canvas.SetTop(control, y * scale);
        }

        public static void MoveElementOnCanvas(FrameworkElement FrameworkElement, double toX, double toY, double scale)
        {
            double fromX = Canvas.GetLeft(FrameworkElement);
            double fromY = Canvas.GetTop(FrameworkElement);

            Storyboard storyboard = new Storyboard();
            DoubleAnimation animationX = CreateDoubleAnimation(FrameworkElement,
                                                fromX, toX * scale, new PropertyPath(Canvas.LeftProperty));
            DoubleAnimation animationY = CreateDoubleAnimation(FrameworkElement,
                                                fromY, toY * scale, new PropertyPath(Canvas.TopProperty));
            storyboard.Children.Add(animationX);
            storyboard.Children.Add(animationY);
            storyboard.Begin();
        }

        public static DoubleAnimation CreateDoubleAnimation(FrameworkElement frameworkElement,
                                     double from, double to, PropertyPath propertyToAnimate)
        {
            return CreateDoubleAnimation(frameworkElement, from, to, propertyToAnimate, TimeSpan.FromMilliseconds(25));
        }

        public static DoubleAnimation CreateDoubleAnimation(FrameworkElement frameworkElement,
                                             double from, double to, PropertyPath propertyToAnimate, TimeSpan timeSpan)
        {
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, frameworkElement);
            Storyboard.SetTargetProperty(animation, propertyToAnimate);
            animation.From = from;
            animation.To = to;
            animation.Duration = timeSpan;
            return animation;
        }

        public static void ResizeElement(FrameworkElement control, double width, double height, double scale)
        {
            if (control.Width != width * scale)
                control.Width = width * scale;
            if (control.Height != height * scale)
                control.Height = height * scale;
        }
    }
}



