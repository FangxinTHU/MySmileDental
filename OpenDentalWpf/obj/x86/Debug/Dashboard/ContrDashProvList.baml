   M S B A M L   `   `   `  ����  OpenDentalWpf%    OpenDentalWpf.ContrDashProvList   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �� -   5        $��
canvasMain��5      .+�5	   
   &   System.Windows.Controls.DataGrid  -   5        $��gridMain��5	        �� Left$  8��6#   
 �� Top$ 34��63   $	��554��6C   $	��284��6P      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     CanUserSortColumns .  	   
IsReadOnly	 . "
   VerticalScrollBarVisibility$
 Visible=�5   u      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Provider��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *
ProvName+4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Color��6)   $ 45 68      CellTemplate 5      �� 5      .R�5      �� $��13��6(   ��5   "   �� 5   &    �� Color 66   �� *	ProvColor+6T   5      5      5        5      $ 
Production��6%   $ 69 69    6D   �� *
Production+   ElementStyle 5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property � �� �� Value$ Right=�6D   5      5        5       $ Income��6%   $ * 65    6?   �� *Income+ 5!      �� 5"      " ��6    .�5#      ��  � ��$ Right=�6D   5$      5&      5(      �� 5)   
   -   5        $��textDate��5)   >   $  96��6   $ 8��6#   $��20��62   $	��101��6N   �� 5*   
   -   5        $��labelR��5*   @   $	  199��6   $ 2��6"   $��>��61   $	��200��6N   $
��Bold"�6z   $��14(�6�   �� 5+   
   -   5        $��labelL��5+   N   $  77��6   $ 2��6!   $��<��60   $	��200��6?   $��Right=�6\   $
��Bold"�6�   $��14(�6�   5,      5-      