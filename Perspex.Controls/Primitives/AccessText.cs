﻿// -----------------------------------------------------------------------
// <copyright file="AccessText.cs" company="Steven Kirk">
// Copyright 2015 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Perspex.Controls.Primitives
{
    using Perspex.Media;

    /// <summary>
    /// A text block that displays a character prefixed with an underscore as an access key.
    /// </summary>
    public class AccessText : TextBlock
    {
        /// <summary>
        /// Defines the <see cref="ShowAccessKey"/> property.
        /// </summary>
        public static readonly PerspexProperty<bool> ShowAccessKeyProperty =
            PerspexProperty.Register<AccessText, bool>("ShowAccessKey", inherits: true);

        /// <summary>
        /// Gets or sets a value indicating whether the access key should be underlined.
        /// </summary>
        public bool ShowAccessKey
        {
            get { return this.GetValue(ShowAccessKeyProperty); }
            set { this.SetValue(ShowAccessKeyProperty, value); }
        }

        /// <summary>
        /// Renders the <see cref="AccessText"/> to a drawing context.
        /// </summary>
        /// <param name="context">The drawing context.</param>
        public override void Render(IDrawingContext context)
        {
            base.Render(context);

            int underscore = this.Text?.IndexOf('_') ?? -1;

            if (underscore != -1 && this.ShowAccessKey)
            {
                var rect = this.FormattedText.HitTestTextPosition(underscore);
                var offset = new Vector(0, -0.5);
                context.DrawLine(
                    new Pen(this.Foreground, 1),
                    rect.BottomLeft + offset,
                    rect.BottomRight + offset);
            }
        }

        /// <summary>
        /// Creates the <see cref="FormattedText"/> used to render the text.
        /// </summary>
        /// <param name="constraint">The constraint of the text.</param>
        /// <returns>A <see cref="FormattedText"/> object.</returns>
        protected override FormattedText CreateFormattedText(Size constraint)
        {
            var result = new FormattedText(
                this.StripAccessKey(this.Text),
                this.FontFamily,
                this.FontSize,
                this.FontStyle,
                this.TextAlignment,
                this.FontWeight);
            result.Constraint = constraint;
            return result;
        }

        /// <summary>
        /// Measures the control.
        /// </summary>
        /// <param name="availableSize">The available size for the control.</param>
        /// <returns>The desired size.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var result = base.MeasureOverride(availableSize);
            return result.WithHeight(result.Height + 1);
        }

        /// <summary>
        /// Returns a string with the first underscore stripped.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The text with the first underscore stripped.</returns>
        private string StripAccessKey(string text)
        {
            var position = text.IndexOf('_');

            if (position == -1)
            {
                return text;
            }
            else
            {
                return text.Substring(0, position) + text.Substring(position + 1);
            }
        }
    }
}
