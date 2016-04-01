using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DiceInvader.Base.Models;
using DiceInvaders.Views;

namespace DiceInvaders.AnimationLayer
{
    public class GameEngineHelper
    {
        /// <summary>
        ///     Returns the full name of the image file representing the invader type
        /// </summary>
        /// <param name="invaderType"> Type of enemy</param>
        /// <returns></returns>
        public string CreateImageName(InvaderType invaderType)
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
        ///     Returns the full name of the image file representing the shot type
        /// </summary>
        /// <param name="shotType">Type of shot</param>
        /// <returns></returns>
        public string CreateImageName(ShotType shotType)
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


        public FrameworkElement InvaderControlFactory(Invader invader, double scale)
        {
            var imageName = CreateImageName(invader.InvaderType);
            var invaderControl = new AnimatedImage(imageName)
            {
                Width = invader.Size.Width*scale,
                Height = invader.Size.Height*scale
            };
            SetCanvasLocation(invaderControl, invader.Location.X, invader.Location.Y, scale);

            return invaderControl;
        }

        internal FrameworkElement ShotControlFactory(Shot shot, double scale)
        {
            var imageName = CreateImageName(shot.ShotType);
            var shotControl = new AnimatedImage(imageName)
            {
                Width = Shot.ShotSize.Width*scale,
                Height = Shot.ShotSize.Height*scale
            };

            SetCanvasLocation(shotControl, shot.Location.X, shot.Location.Y, scale);
            return shotControl;
        }


        internal FrameworkElement PlayerControlFactory(Player player, double scale)
        {
            var playerControl = new AnimatedImage("player.png")
            {
                Width = player.Size.Width*scale,
                Height = player.Size.Height*scale
            };
            SetCanvasLocation(playerControl, player.Location.X, player.Location.Y, scale);
            return playerControl;
        }

        public void SetCanvasLocation(FrameworkElement control, double x, double y, double scale)
        {
            Canvas.SetLeft(control, x*scale);
            Canvas.SetTop(control, y*scale);
        }

        public void MoveElementOnCanvas(FrameworkElement frameworkElement, double toX, double toY, double scale)
        {
            var fromX = Canvas.GetLeft(frameworkElement);
            var fromY = Canvas.GetTop(frameworkElement);

            var storyboard = new Storyboard();
            var animationX = CreateDoubleAnimation(frameworkElement,
                fromX, toX*scale, new PropertyPath(Canvas.LeftProperty));
            var animationY = CreateDoubleAnimation(frameworkElement,
                fromY, toY*scale, new PropertyPath(Canvas.TopProperty));
            storyboard.Children.Add(animationX);
            storyboard.Children.Add(animationY);
            storyboard.Begin();
        }

        public DoubleAnimation CreateDoubleAnimation(FrameworkElement frameworkElement,
            double from, double to, PropertyPath propertyToAnimate)
        {
            return CreateDoubleAnimation(frameworkElement, from, to, propertyToAnimate, TimeSpan.FromMilliseconds(25));
        }

        public DoubleAnimation CreateDoubleAnimation(FrameworkElement frameworkElement,
            double from, double to, PropertyPath propertyToAnimate, TimeSpan timeSpan)
        {
            var animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, frameworkElement);
            Storyboard.SetTargetProperty(animation, propertyToAnimate);
            animation.From = from;
            animation.To = to;
            animation.Duration = timeSpan;
            return animation;
        }

        public void ResizeElement(FrameworkElement control, double width, double height, double scale)
        {
            if (control.Width != width*scale)
                control.Width = width*scale;
            if (control.Height != height*scale)
                control.Height = height*scale;
        }
    }
}