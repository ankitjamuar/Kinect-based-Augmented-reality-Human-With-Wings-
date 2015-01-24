//------------------------------------------------------------------------------
// Author: Ankit Sinha
// Description: Kinect based Augmented reality, it draw wings on back of detected human
// 
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.BackgroundRemovalBasics
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Toolkit;
    using Microsoft.Kinect.Toolkit.BackgroundRemoval;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        bool error = false;

        /// <summary>
        /// Width of output drawing
        /// </summary>
        private const float RenderWidth = 640.0f;

        /// <summary>
        /// Height of our output drawing
        /// </summary>
        /// 
        private const float RenderHeight = 480.0f;

        /// <summary>
        /// Format we will use for the depth stream
        /// </summary>
        private const DepthImageFormat DepthFormat = DepthImageFormat.Resolution320x240Fps30;

        /// <summary>
        /// Format we will use for the color stream
        /// </summary>
        private const ColorImageFormat ColorFormat = ColorImageFormat.RgbResolution640x480Fps30;

        /// <summary>
        /// Bitmap that will hold color information
        /// </summary>
        private WriteableBitmap foregroundBitmap;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensorChooser sensorChooser;

        /// <summary>
        /// Our core library which does background 
        /// </summary>
        private BackgroundRemovedColorStream backgroundRemovedColorStream;

        /// <summary>
        /// Intermediate storage for the skeleton data received from the sensor
        /// </summary>
        private Skeleton[] skeletons;

        /// <summary>
        /// the skeleton that is currently tracked by the app
        /// </summary>
        private int currentlyTrackedSkeletonId;

        /// <summary>
        /// Track whether Dispose has been called
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Drawing group for skeleton rendering output
        /// </summary>
        private DrawingGroup drawingGroup;

        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSource;

        private KinectChangedEventArgs arg;

        private DepthImagePixel[] depthPixels;

        //WINGS
        private BitmapImage img;
        private BitmapImage img2;
       

    
        public MainWindow()
        {
            this.InitializeComponent();

            // initialize the sensor chooser and UI
            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.KinectChanged += this.SensorChooserOnKinectChanged;
            this.sensorChooser.Start();


            // Create the drawing group we'll use for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            this.imageSource = new DrawingImage(this.drawingGroup);

            // Display the drawing using our image control
            Backdrop.Source = this.imageSource;

            video.Source = new Uri(System.IO.Directory.GetCurrentDirectory() + "/images/redang.wmv");
            video.Play();

           
            //RENDER WING
            img = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/images/1.png"));
            img2 = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/images/2.png"));


        }

      
        ~MainWindow()
        {
            this.Dispose(false);
        }

      
        public void Dispose()
        {
            this.Dispose(true);

            // This object will be cleaned up by the Dispose method.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (null != this.backgroundRemovedColorStream)
                {
                    this.backgroundRemovedColorStream.Dispose();
                    this.backgroundRemovedColorStream = null;
                }

                this.disposed = true;
            }
        }


        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.sensorChooser.Stop();
            this.sensorChooser = null;
        }


        private void SensorAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            // in the middle of shutting down, or lingering events from previous sensor, do nothing here.
            if (null == this.sensorChooser || null == this.sensorChooser.Kinect || this.sensorChooser.Kinect != sender)
            {
                return;
            }

            try
            {
                using (var depthFrame = e.OpenDepthImageFrame())
                {
                    if (null != depthFrame)
                    {
                        this.backgroundRemovedColorStream.ProcessDepth(depthFrame.GetRawPixelData(), depthFrame.Timestamp);
                        depthFrame.CopyDepthImagePixelDataTo(this.depthPixels);
                
                    }
                }

                using (var colorFrame = e.OpenColorImageFrame())
                {
                    if (null != colorFrame)
                    {
                        this.backgroundRemovedColorStream.ProcessColor(colorFrame.GetRawPixelData(), colorFrame.Timestamp);
                    }
                }

                using (var skeletonFrame = e.OpenSkeletonFrame())
                {
                    if (null != skeletonFrame)
                    {
                        skeletonFrame.CopySkeletonDataTo(this.skeletons);
                        this.backgroundRemovedColorStream.ProcessSkeleton(skeletons, skeletonFrame.Timestamp);

                        if (skeletons.Length != 0)
                        {
                            foreach (Skeleton skel in skeletons)
                            {

                                if (skel.TrackingState == SkeletonTrackingState.Tracked)
                                {

                                    using (DrawingContext drawingContext = this.drawingGroup.Open())
                                    {

                                        // Draw a transparent background to set the render size
                                        drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));


                                        Joint wristRight = skel.Joints[JointType.HandRight];
                                        Joint shoulderRight = skel.Joints[JointType.ShoulderCenter];

                                        double hRightY = System.Math.Round(wristRight.Position.Y, 2);
                                        double sRightY = System.Math.Round(shoulderRight.Position.Y, 2);

                                        DepthImagePoint depthPoint = this.arg.NewSensor.CoordinateMapper.MapSkeletonPointToDepthPoint(wristRight.Position, DepthImageFormat.Resolution640x480Fps30);



                                        Rect start = new Rect(this.SkeletonPointToScreen(shoulderRight.Position),new Point(depthPoint.X+50,depthPoint.Y+50));
                                        
                                        if (hRightY <= sRightY)
                                            drawingContext.DrawImage(img, start);


                                        
                                        Joint wristLeft = skel.Joints[JointType.HandLeft];
                                        Joint shoulderLeft = skel.Joints[JointType.ShoulderCenter];

                                        double hLeftY = System.Math.Round(wristLeft.Position.Y, 2);
                                        double sLeftY = System.Math.Round(shoulderLeft.Position.Y, 2);

                                        DepthImagePoint depthPoint2 = this.arg.NewSensor.CoordinateMapper.MapSkeletonPointToDepthPoint(wristLeft.Position, DepthImageFormat.Resolution640x480Fps30);


                                        Rect start2 = new Rect(this.SkeletonPointToScreen(shoulderLeft.Position), new Point(depthPoint2.X - 50, depthPoint2.Y + 50));
                                        //Rect start2 = new Rect(this.SkeletonPointToScreen(shoulderLeft.Position), this.SkeletonPointToScreen(wristLeft.Position));

                                        if (hLeftY <= sLeftY)
                                            drawingContext.DrawImage(img2, start2);

                                    }


                                }
                                else if (skel.TrackingState == SkeletonTrackingState.PositionOnly)
                                {

                                }
                            }
                        }
                    
                    }
                    
                }

                this.ChooseSkeleton();
            }
            catch (InvalidOperationException)
            {
                // Ignore the exception. 
            }
        }

        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            try
            {
                DepthImagePoint depthPoint = this.arg.NewSensor.CoordinateMapper.MapSkeletonPointToDepthPoint(
                    skelpoint,
                    DepthImageFormat.Resolution640x480Fps30);
                return new Point(depthPoint.X, depthPoint.Y);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.ToString());
                return new Point(100, 100);
            }
           
        }



        private void BackgroundRemovedFrameReadyHandler(object sender, BackgroundRemovedColorFrameReadyEventArgs e)
        {
            using (var backgroundRemovedFrame = e.OpenBackgroundRemovedColorFrame())
            {
                if (backgroundRemovedFrame != null)
                {
                    if (null == this.foregroundBitmap || this.foregroundBitmap.PixelWidth != backgroundRemovedFrame.Width 
                        || this.foregroundBitmap.PixelHeight != backgroundRemovedFrame.Height)
                    {
                        this.foregroundBitmap = new WriteableBitmap(backgroundRemovedFrame.Width, backgroundRemovedFrame.Height, 96.0, 96.0, PixelFormats.Bgra32, null);

                        // Set the image we display to point to the bitmap where we'll put the image data
                        this.MaskedColor.Source = this.foregroundBitmap;
                    }

                    // Write the pixel data into our bitmap
                    this.foregroundBitmap.WritePixels(
                        new Int32Rect(0, 0, this.foregroundBitmap.PixelWidth, this.foregroundBitmap.PixelHeight),
                        backgroundRemovedFrame.GetRawPixelData(),
                        this.foregroundBitmap.PixelWidth * sizeof(int),
                        0);
                }
            }
        }

      
        private void ChooseSkeleton()
        {
            var isTrackedSkeltonVisible = false;
            var nearestDistance = float.MaxValue;
            var nearestSkeleton = 0;

            foreach (var skel in this.skeletons)
            {
                if (null == skel)
                {
                    continue;
                }

                if (skel.TrackingState != SkeletonTrackingState.Tracked)
                {
                    continue;
                }

                if (skel.TrackingId == this.currentlyTrackedSkeletonId)
                {
                    isTrackedSkeltonVisible = true;
                    break;
                }

                if (skel.Position.Z < nearestDistance)
                {
                    nearestDistance = skel.Position.Z;
                    nearestSkeleton = skel.TrackingId;
                }
            }

            if (!isTrackedSkeltonVisible && nearestSkeleton != 0)
            {
                this.backgroundRemovedColorStream.SetTrackedPlayer(nearestSkeleton);
                this.currentlyTrackedSkeletonId = nearestSkeleton;
            }
        }

        private void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.AllFramesReady -= this.SensorAllFramesReady;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.ColorStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();

                    // Create the background removal stream to process the data and remove background, and initialize it.
                    if (null != this.backgroundRemovedColorStream)
                    {
                        this.backgroundRemovedColorStream.BackgroundRemovedFrameReady -= this.BackgroundRemovedFrameReadyHandler;
                        this.backgroundRemovedColorStream.Dispose();
                        this.backgroundRemovedColorStream = null;
                    }
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }

            if (args.NewSensor != null)
            {
                try
                {
                    this.arg = args;
                    args.NewSensor.DepthStream.Enable(DepthFormat);
                    args.NewSensor.ColorStream.Enable(ColorFormat);
                    args.NewSensor.SkeletonStream.Enable();

                    this.backgroundRemovedColorStream = new BackgroundRemovedColorStream(args.NewSensor);
                    this.backgroundRemovedColorStream.Enable(ColorFormat, DepthFormat);

                    // Allocate space to put the depth pixels we'll receive
                    this.depthPixels = new DepthImagePixel[args.NewSensor.DepthStream.FramePixelDataLength];


                    // Allocate space to put the depth, color, and skeleton data we'll receive
                    if (null == this.skeletons)
                    {
                        this.skeletons = new Skeleton[args.NewSensor.SkeletonStream.FrameSkeletonArrayLength];
                    }

                    // Add an event handler to be called when the background removed color frame is ready, so that we can
                    // composite the image and output to the app
                    this.backgroundRemovedColorStream.BackgroundRemovedFrameReady += this.BackgroundRemovedFrameReadyHandler;

                    // Add an event handler to be called whenever there is new depth frame data
                    args.NewSensor.AllFramesReady += this.SensorAllFramesReady;

                    try
                    {
                        args.NewSensor.DepthStream.Range = this.checkBoxNearMode.IsChecked.GetValueOrDefault()
                                                    ? DepthRange.Near
                                                    : DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                    }
                    catch (InvalidOperationException)
                    {
                        // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                        error = true;
                    }

                    this.statusBarText.Text = Properties.Resources.ReadyForScreenshot;
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
                
            }
        }
      
        private void ButtonScreenshotClick(object sender, RoutedEventArgs e)
        {
            /*
            if (null == this.sensorChooser || null == this.sensorChooser.Kinect)
            {
                this.statusBarText.Text = Properties.Resources.ConnectDeviceFirst;
                return;
            }

            int colorWidth = this.foregroundBitmap.PixelWidth;
            int colorHeight = this.foregroundBitmap.PixelHeight;

            // create a render target that we'll render our controls to
            var renderBitmap = new RenderTargetBitmap(colorWidth, colorHeight, 96.0, 96.0, PixelFormats.Pbgra32);

            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                // render the backdrop
                var backdropBrush = new VisualBrush(Backdrop);
                dc.DrawRectangle(backdropBrush, null, new Rect(new Point(), new Size(colorWidth, colorHeight)));

                // render the color image masked out by players
                var colorBrush = new VisualBrush(MaskedColor);
                dc.DrawRectangle(colorBrush, null, new Rect(new Point(), new Size(colorWidth, colorHeight)));
            }

            renderBitmap.Render(dv);
    
            // create a png bitmap encoder which knows how to save a .png file
            BitmapEncoder encoder = new PngBitmapEncoder();

            // create frame from the writable bitmap and add to encoder
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            var time = DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

            var myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            var path = Path.Combine(myPhotos, "KinectSnapshot-" + time + ".png");

            // write the new file to disk
            try
            {
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    encoder.Save(fs);
                }

                this.statusBarText.Text = string.Format(CultureInfo.InvariantCulture, Properties.Resources.ScreenshotWriteSuccess, path);
            }
            catch (IOException)
            {
                this.statusBarText.Text = string.Format(CultureInfo.InvariantCulture, Properties.Resources.ScreenshotWriteFailed, path);
            }
             * */
        }
         
        private void CheckBoxNearModeChanged(object sender, RoutedEventArgs e)
        {
            if (null == this.sensorChooser || null == this.sensorChooser.Kinect)
            {
                return;
            }

            // will not function on non-Kinect for Windows devices
            try
            {
                this.sensorChooser.Kinect.DepthStream.Range = this.checkBoxNearMode.IsChecked.GetValueOrDefault()
                                                    ? DepthRange.Near
                                                    : DepthRange.Default;
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
