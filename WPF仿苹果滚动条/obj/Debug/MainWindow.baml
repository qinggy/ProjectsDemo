   M S B A M L   `   `   `  ����  WPF仿苹果滚动条&     WPF仿苹果滚动条.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	    � 	Resources 5         NormalColor  HighlightColor  PressedColor  BorderColor  	BackColor  stBorder �� 
TargetType �� Property �� Value  stBackBorder  stRepeatBtn �� 
TargetType �� Path �� RelativeSource �� Triggers	 P� Property
 P� Value �� 
TargetName  stScrollBtn 	 stThumb 
 VerticalScrollBar � RowDefinitions �� CornerRadius ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton  HorizontalScrollBar � ColumnDefinitions �� VerticalOffset � Value �� ScrollableHeight � Maximum �� ViewportHeight �� ViewportSize* �� #ComputedVerticalScrollBarVisibility �� Orientation �� HorizontalOffset �� ScrollableWidth �� ViewportWidth, �� %ComputedHorizontalScrollBarVisibility��%/  &	        &	      &	 9     &	 X     &	 t     &	 �     7  7   &	 G    7  7  &	 �    7  7  7  &	 x    &		 �    7  7  7  &	
 �    7  7  7  7 	 7  7  &	 �
    7  7  7  7 	 7  7  '�� �    7  7 
 7  ��5   
   Red��63   ��5   
   
Orange��69   ��5   
   DarkRed��68   ��5	   
   Gray��64   ��5
   
   Pink��62   �� 5   
   " ��6!   .�5      ��  � ��8   6,   �� 5       � ��$ 1q�60   �� 5       � ��8  6+   �� 5       �@��CornerRadius$ 3��6-   5      �� 5   
   " ��6%   .�5      ��  � ��8   6,   �� 5       � ��$ 1q�60   �� 5       � ��8  6+   �� 5       �@��CornerRadius$ 3��6-   5      �� 5   
   " ��6$   .�5      ��  �@J�SnapsToDevicePixels . 64   �� 5       �@�OverridesDefaultStyle . 66   �� 5       � |� .  6*   �� 5       � �� 5      �� 5      " ��6&   .R�5       �� $��bd��6!   8��  ._�5"      F� $��Center=�5#   !   $��Center=�5$   !   
���������5&   !   �� $ Content� �� *TemplatedParent+5(       5)      P� 5*      	 �@J�IsMouseOver
 . . �5+   "   �� $ bd��6)   8   � ��6O   5,      P� 5-      	 � ��
 . . �5.   "   �� $ bd��6)   8   � ��6O   5/      51      53      54      �� 56   
   " ��6$   .�57      ��  �@J�SnapsToDevicePixels . 64   �� 58       �@�OverridesDefaultStyle . 66   �� 59       � �� .  6*   �� 5:       � |� .  6*   �� 5;       � �� 5<      �� 5=      " ��6&   .R�5>      �� 
������ 6!   5?      5A      5B      �� 5D   
   " n�6    .�5E      ��  �@J�SnapsToDevicePixels . 64   �� 5F       �@�OverridesDefaultStyle . 66   �� 5G       � �� .  6*   �� 5H       � |� .  6*   �� 5I       � �� 5J      �� 5K      " n�6&   .R�5L      �� $��bd��6!   8��  6O    5M      P� 5N      	 �@n�
IsDragging
 . . �5O   "   �� $ bd��6)   8   � ��6w   5P      P� 5Q      	 �@J�IsMouseOver
 . . �5R   "   �� $ bd��6)   8   � ��6y   5S      5U      5W      5X      �� 5Z   
   " ��64   .R�5[      �� 8��  ._�5\      �  5]      �� 5^      �� 5_      $��18��6(   �� 5`      $��18��6(   .+�5b      �� $��3��5c      $ 2��5d      �� 5e      $��1��5f      8�� $��18��5h      $��ScrollBar.LineUpCommand��5i      $��M 0 4 L 8 4 L 4 0 Z��5j      ]� 5k      $��
PART_Track��5l      $��0��5m      ��.  5o      �� 5p      8�� $��ScrollBar.PageUpCommand��5r       5t      n� 5u      8�� $��1,0,1,0q�5w       5y      �� 5z      8�� $��ScrollBar.PageDownCommand��5|      5~      �� 5      $��3��5�      8�� $��18��5�      $��ScrollBar.LineDownCommand��5�      $��M 0 0 L 4 4 L 8 0 Z��5�      5�      5�      5�      �� 5�   
   " ��66   .R�5�      �� 8��  ._�5�      �  5�      �� 5�      �� 5�      $��18��6+   �� 5�      $��18��6+   .+�5�      �� $��3��5�      $ 2��5�      �� 5�      $��1��5�      8�� $��18��5�      $��ScrollBar.LineLeftCommand��5�      $��M 4 0 L 4 8 L 0 4 Z��5�      ]� 5�      $��
PART_Track��5�      $��0��5�      ��.   5�      �� 5�      8�� $��ScrollBar.PageLeftCommand��5�       5�      n� 5�      8�� $��0,1,0,1q�5�       5�      �� 5�      8�� $ ��ScrollBar.PageRightCommand��5�      5�      �� 5�      $��2��5�      8�� $��18��5�      $ ��ScrollBar.LineRightCommand��5�      $��M 0 0 L 4 4 L 0 8 Z��5�      5�      5�      5�      �� 5�   
   " ��6   .�5�      ��  �@�OverridesDefaultStyle . 66   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      � 8��   5�      �� 5�   "   �� 5�   "   $
��Auto �63    5�      �� 5�   "   �� 5�   "   $
��Auto �60   .+�5�      �� �� 5�      $��PART_VerticalScrollBar��6(   $��1��5�   (   # z 5�   (   # z 5�   (   # z 5�   (   #y�z 5�   (   8�� 5�   (   �� 5�      $��PART_HorizontalScrollBar��6(   $ 
Horizontal=�5�   (   $��1��5�   (   # z 5�   (   # z 5�   (   # z 5�   (   #y�z 5�   (   8�� 5�   (   5�      5�      5�      5�      5�      .��5�      �� $
��Auto=�6   .��5�   
   � .+�5�      �� $	��500��6   $	��500��5�      ��5�      !� 5�       j� ViewportUnits$ Absolute=�6"    j� Viewport$ 	0 0 50 30�5�   "     j� TileMode$  FlipXY=�5�   "   ! !� Visual! 5�      �� 5�      $��5q�6%   $��50��60   $��30��6;   .��6G   ASD6L   5�      5�      5�      5�      5�      