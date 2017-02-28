using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace BitSynthPlus.Controls
{
    /// <summary>
    /// A class representing each Key object
    /// Derives from basic XAML Control class
    /// Override events and Pointer List enables Key touch behavior for sliding across a key to activate
    /// </summary>
    public class Key : Control
    {
        public delegate void ValueChangedEventHandler(object sender, EventArgs e);

        public event ValueChangedEventHandler IsPressedPropertyChanged;

        static readonly DependencyProperty isPressedProperty =
            DependencyProperty.Register("IsPressed",
                typeof(bool), typeof(Key),
                new PropertyMetadata(false, OnIsPressedChanged));

        List<uint> pointerList = new List<uint>();

        public static DependencyProperty IsPressedProperty
        {
            get { return isPressedProperty; }
        }

        /// <summary>
        /// Gets or sets whether or not a Key is pressed
        /// </summary>
        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }

        /// <summary>
        /// Overriding OnPointerEntered to behave more like typical Pressed event
        /// </summary>
        /// <param name="args"></param>
        protected override void OnPointerEntered(PointerRoutedEventArgs args)
        {
            if (args.Pointer.IsInContact)
                AddToList(args.Pointer.PointerId);
            base.OnPointerEntered(args);

            ChangeIsPressedProperty();
        }

        /// <summary>
        /// Overriding OnPointerPressed to keep track of Pointer list
        /// </summary>
        /// <param name="args"></param>
        protected override void OnPointerPressed(PointerRoutedEventArgs args)
        {
            AddToList(args.Pointer.PointerId);
            base.OnPointerPressed(args);

            ChangeIsPressedProperty();
        }

        /// <summary>
        /// Overriding OnPointerReleased to keep track of Pointer list
        /// </summary>
        /// <param name="args"></param>
        protected override void OnPointerReleased(PointerRoutedEventArgs args)
        {
            RemoveFromList(args.Pointer.PointerId);
            base.OnPointerReleased(args);

            ChangeIsPressedProperty();
        }

        /// <summary>
        /// Overriding OnPointerExited to keep track of Pointer list
        /// </summary>
        /// <param name="args"></param>
        protected override void OnPointerExited(PointerRoutedEventArgs args)
        {
            RemoveFromList(args.Pointer.PointerId);
            base.OnPointerExited(args);

            ChangeIsPressedProperty();
        }

        /// <summary>
        /// Adds a Pointer Id to the Pointer List
        /// </summary>
        /// <param name="id"></param>
        void AddToList(uint id)
        {
            if (!pointerList.Contains(id))
                pointerList.Add(id);

            CheckList();
        }

        /// <summary>
        /// Removes a Pointer Id from the Pointer List
        /// </summary>
        /// <param name="id"></param>
        void RemoveFromList(uint id)
        {
            if (pointerList.Contains(id))
                pointerList.Remove(id);

            CheckList();
        }

        /// <summary>
        /// Sets IsPressed if Pointer List count is greater than 0
        /// </summary>
        void CheckList()
        {
            this.IsPressed = pointerList.Count > 0;
        }

        public static void OnIsPressedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            VisualStateManager.GoToState(obj as Key,
                (bool)args.NewValue ? "Pressed" : "Normal", false);
        }

        private void ChangeIsPressedProperty()
        {
            if (IsPressedPropertyChanged != null)
            {
                IsPressedPropertyChanged(this, EventArgs.Empty);
            }
        }
    }
}
