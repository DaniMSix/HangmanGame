   M S B A M L   `   `   `  ����@  <Views, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Views.PagesViews.Pages  5      3clr-namespace:Views.PropertiesViews.Properties  5           Views.Pages.PageStatistics   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Views.Pages     5      ,pclr-namespace:Views.Properties     5        N� Title$  PageStatistics��5
      .��5      � ��5   
   �� 5       �� ImageSource$' !\Images\Background-statistics.png��6   .+�5   
   �� -   5         ��Name$ lbStadistics��5         FuenteVT323#��[  6&       Views.Properties.Resources   lbStadistics#��Z 6P   $��Center=�6~   $��22(�6�   ���#6D3B00$
��Left=�6�   $��429,208,0,0q�6�   $	��Top=�6�   $	��187��6  �� 5   
   -   5        $ 
lbGamesWon��5        PressStart2P#��[ 6$   
�������$��25(�6b   $
��Left=�6p   $��370,115,0,0q�6�   $	��Top=�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGridStatistics��5         AutoGenerateColumns .  $��272,252,219,41q�6K   
������ 
���   �
������    GridLinesVisibility$
 None=�5   '      HeadersVisibility$
 None=�6B      SelectionMode$ Single=�6[   	 � 	Resources	 5      *  $System.Windows.Controls.DataGridCell
 �� 
TargetType �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%  '        '  p     �� 5      "
  6   .�5      ��  � ��
 ���� 63   �� 5       � ��
 ���� 64   5      �� 5      "
  6   .�5      ��  � ��
 ���� 63   �� 5       � ��
 ���� 64   �� 5       � ��
 �   �63   5       5!         Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ Nombre de Usuario��6%      
IsReadOnly .    
FontFamily# [  6v      FontSize$ 18Q�6�      
Foreground �#A77A45   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5$      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5#   @   �� *usernameGuesser+5$        5%      $ 	Resultado��6%    . # [  6h   $ 18Q�6�    �#A77A45$ * 5&       5%   8   �� *	winSymbol+5&        5'      $ Fecha de Creación��6%    . # [  6�   $ 18Q�6�    �#A77A45$ * 5(       5'   @   �� *
dateGame+ �� StringFormat$ {0:dd/MM/yyyy}��5(         RowStyle 5*      �� 5+      )	  #System.Windows.Controls.DataGridRow"
 	 6   .�5,      ��  � ��
 ���� 63   �� 5-       � ��$	 0 5q�6/    �� Triggers 5/      P� 50       P� Property �@	 
IsSelected P� Value . . �51      ��  � �� �#DED4B16;   52      54      56      �� 57   
   -   5         � Cursor$
 Hand��57        TransparentButtonStyle#��[ 6   $��4,5,913,428q�6O   .��58      �� $��\Images\arrow-left.png��6   $��65��64   $��54��6?   59       � 5;   
   -   5          �Name$ frMenu��5;      $
��Left=�6    $	��500��6;   $��	784,0,0,0q�6H   $	��Top=�6[   $	��211��6s   $y�	Collapsed=�6    � 5<   
   -   5        $ frHome��5<      $��Stretch=�6    $��Stretch=�6>   $��0,0,0,0q�6Z   �� 5=   
   $��\Images\icon-win.png��6   $
��Left=�6.   $��90��6I   $��
300,80,0,0q�6U   $	��Top=�6i   $��80��6�   5>      5?      