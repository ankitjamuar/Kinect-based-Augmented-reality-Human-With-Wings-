   M S B A M L   `   `   `  ����  BackgroundRemovalBasics-WPFA    ;Microsoft.Samples.Kinect.BackgroundRemovalBasics.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   S OMicrosoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=nullh dMicrosoft.Kinect.Toolkit.Controls, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e352k(http://schemas.microsoft.com/kinect/2013   5   	   -   5          � Title$  Background Removal Basics��5   	   $	��735��6+   $	��770��68    � 	Resources 5	         MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush �� 
TargetType �� Property �� Value  SnapshotButton �� 
TargetType �� Orientation ��Name	 �� TextAlignment
 ��Name �� Triggers P� Property P� Value �� 
TargetName  SquareCheckBox �Name��%w
  &	        &	 #     &	 F     '�� i     &	 �     7  7  &	 �    7  7  �� 5
   
   $ 	#ff6e6e6e��62   �� 5   
   $ 	#ff52318f��64   �� 5   
   $ 	#ff00BCF2��62   �� 5   
   " ��6   .�5      ��  �@J�SnapsToDevicePixels . 64   5      �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� $ 
Horizontal=�6)   
������ .+�5   "   �� $ SnapText��6,   #��z��6>   $
	 Left=�6_   $��Center=�6u   8��  $��15(�6�   � 5   "   $��9,0,0,0q�6'   .+�5   &   �� $
 
SnapNormal��6,   $��Images\SnapNormal.png��6@   $
��None=�6_   $��Center=�6n   �� 5   &   $
 	SnapHover��6,   $��Images\SnapHover.png��6?   $
��None=�6]   $��Center=�6l   $y�	Collapsed=�6�   5   #   5      5       5      P� 5       �@J�IsMouseOver . . �5   "   �� $ 
SnapNormal��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5    "   $ 	SnapHover��6O    � y�$ Visible=�6?   6O   �� 5!   "   8  $ SnapText��6h    � ��5"      5$      5&      5'      �� 5)   
   " ��6   .�5*      ��  � �� 5+      �� 5,      " ��6&   .R�5-      � .+�5.      �� $ 
Horizontal=�6)   
������ .+�5/   "   � $ SquareCheckBoxChecked��6'   .+�50   &   �� $
 CheckedNormal��6,   $��Images\CheckedNormal.png��6C   $
��None=�6e   $��Center=�6t   �� 51   &   $
 CheckedHover��6,   $��Images\CheckedHover.png��6B   $
��None=�6c   $��Center=�6r   $y�	Collapsed=�6�   52   #   � 53   "   $ SquareCheckBoxUnchecked��6'   $y�	Collapsed=�6H   .+�54   &   �� $
 UncheckedNormal��6,   $ ��Images\UncheckedNormal.png��6E   $
��None=�6i   $��Center=�6x   �� 55   &   $
 UncheckedHover��6,   $��Images\UncheckedHover.png��6D   $
��None=�6g   $��Center=�6v   $y�	Collapsed=�6�   56   #   �� 57   "   $ SquareCheckBoxText��6,   #��z��6H   $
	 Left=�6i   $��Center=�6   8��  $��15(�6�   $��9,0,0,0q�6�   58      59       5:      P� 5;       �@d�	IsChecked$ falseY�6;   . �5<   "   �� $ SquareCheckBoxChecked��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5=   "   $ SquareCheckBoxUnchecked��6O    � y�$ Visible=�6?   6O   5>      P� 5?       �@J�IsMouseOver . . �5@   "   �� $ CheckedNormal��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5A   "   $ UncheckedNormal��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5B   "   $ CheckedHover��6O    � y�$ Visible=�6?   6O   �� 5C   "   $ UncheckedHover��6O    � y�$ Visible=�6?   6O   �� 5D   "   8  $ SquareCheckBoxText��6h    � ��5E      5G      5I      5J      5K      .��5M      � -   5        $��
layoutGrid��5M      $��	10 0 10 0q�6    � RowDefinitions 5N   
   �� 5O      $
��Auto �6   �� 5P      $��* �6   �� 5Q      $
��Auto �6   �� 5R      $
��Auto �6   .+�5T   
   � $��0��6   $��0 0 0 20q�6    � ColumnDefinitions 5U      �� 5V      $��* �6#   �� 5W      $��* �6#   �� 5X      $��* �6#   .+�5Z      �� $��0��6   $��Images\Logo.png��6$   $
��Fill=�6=   $��32��6L   $��81��6X   $
��Left=�6c   $��0 10 0 5q�6~   �� 5[      $��2��6   $��0 0 -1 0q�6(   $��Right=�6:   $��Bottom=�6V   #��[  6q   $��Segoe UI)�6�   $��18(�6�   Background Removal Basics6�   4  .Microsoft.Kinect.Toolkit.KinectSensorChooserUI  5\      -   5        $��sensorChooserUi��5\   |   $��1��6&   $��Center=�66   $��0 0 0 5q�6S   $	��Top=�6d   6|   �� 5]      -   5        $��Depth��5]   t   $��65��6   $
��Left=�6$   $��	166,0,0,0q�6?   $��0��6S   $	��Top=�6\   $	��148��6�   $��50(�6�   $��2��6�   5^      � 5_   
   $��1��6   .+�5`      r� -   5        $��video��5`       r� LoadedBehavior$ Manual=�6*    r� Stretch$
 Fill=�6B   $
��1366��6Q   $	��768��6^   �� 5a      -   5        $��Backdrop��5a      �� 5b      -   5        $��MaskedColor��5b      4  .Microsoft.Kinect.Toolkit.Controls.KinectRegion  5c      -   5          Name$ kinectRegion��5c      $y�Hidden=�63   .��5d      � .+�5e      8  2Microsoft.Kinect.Toolkit.Controls.KinectUserViewer  -	   5        $��
userViewer��5e   `   $��Bottom=�6)   $��Right=�6D      UserColoringMode$ HighlightPrimary=�5f         PrimaryUserColor$ Violet��65   $��65��6O   $��	0,0,6,240q�5g   R   $��74��6e      KinectRegion 6   ��  �� ElementName$ kinectRegion��6e   8  2Microsoft.Kinect.Toolkit.Controls.KinectTileButton  5h      -
   5          Name$ ClickMe��5h   )   $	��Top=�6=   $
��Left=�6U   $	��193��6p   $	��331��6}   $��
46,420,0,0q�6�   6�     5i      -   5        $ ClickMe2��5i   )   $	��Top=�6?   $
��Left=�6W   $	��193��6r   $	��350��6   $��488,420,0,0q�6�   6�     5j      -   5        $ ClickMe3��5j   )   $	��Top=�6>   $
��Left=�6V   $	��193��6q   $	��312��6~   $��976,420,0,0q�6�   6�   :  4Microsoft.Kinect.Toolkit.Controls.KinectScrollViewer  5k      $��Disabled=�6+   $
��Auto=�6R   $��Center=�6w   .��5l      �� -   5         ��Name$ StackPanelWithButton��5l   >   $ 
Horizontal=�6%   6>   5m      5n      5o      5q      �� 5r   
   -   5        $��buttonScreenshot��5r   �   $��2��6   #��[ 6   $��
Screenshot��6F   $
��Auto��6[   $��Right=�6i   $��Center=�6�   $��
10 10 0 10q�6�   6�   �� 5s   
   -   5        $��checkBoxNearMode��5s   �   $��2��6   #��[ 6    $��	Near Mode��6H   $
��Auto��6\   $
��Left=�6j   $��Center=�6�   $��
0 10 10 10q�6�   6�   �� 5t   
   -   5        $��	statusBar��5t   ?   $��3��6   $��Stretch=�6!   $��Bottom=�6P   
�������#��[  6~   .@�5u      �� $��0 0 0 10q�6   .��5v      �� -   5        $��statusBarText��5v      $��-1 0 0 0q�61   JHPress 'Screenshot' to save a screenshot to your 'My Pictures' directory.6�   5w      5x      5y      5z      