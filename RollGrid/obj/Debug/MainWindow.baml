   M S B A M L   `   `   `  ����  RollGrid    RollGrid.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � 	Resources 5         
storyboard W� From	 W� To g� Duration g� RepeatBehavior �� 
TargetName �� TargetProperty��%�   &	        �� 5   
   .��5      W� $ 0X�6   $
 -800X�6'   $ 0:0:3D�61   $ Forever��5   )   $ CanvasContainer��5	   )   $ RenderTransform.X�5
   )   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� -   5        	 ��Name$	 pasue��5      $��暂停��6   $��60��6-   $��183,0,274,0q�68   �� 5   
   -   5        $	 resume��5      $��继续��6   $��60��6.   $��	160 0 0 0q�69   �� 5   
   -   5        $��Button��5      $��60��6"   $��
71,0,386,0q�6-   
 J� RenderTransformOrigin$
 -2.383,0.421&�6V   �� 5   
   -   5         ��Name$ prompt��5      $��Center=�6$   
��� � �$$��小的们，滚起来。。。��6R   $��Right=�6d   $��1��6�   z�5      T� 5      5       � Triggers 5   
   :� 5       :� RoutedEvent$ Loaded��6   .[�5      �� -   5         ��Name$ showbtn��5   "   .��5      �� .��5       W� $ 0X�6*   $
 -800X�63   $ 0:0:10D�6=   $ Forever��5!   )   $ prompt��5"   )   $ RenderTransform.X�5#   )   5$      5%      5&      :� 5'      $ TextBlock.MouseEnter��6   .[�5(      ?�  �� BeginStoryboardName$ showbtn��6"   5)      :� 5*      $ TextBlock.MouseLeave��6   .[�5+      �� $ showbtn��6#   5,      5/      50      