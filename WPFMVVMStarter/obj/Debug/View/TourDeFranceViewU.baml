   M S B A M L   `   `   `  ����I  EWPFMVVMStarter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WPFMVVMStarter.ViewWPFMVVMStarter.View  5      ;"clr-namespace:WPFMVVMStarter.ModelWPFMVVMStarter.Model  5      +    %WPFMVVMStarter.View.TourDeFranceViewU   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:WPFMVVMStarter.View     5      4model"clr-namespace:WPFMVVMStarter.Model     5        � 	Resources  5
       �� 
TargetType �� Property �� Value��%�   '��       '�� H     �� 5   
   " ��6   .�5      ��  � ��$ 16(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 5,0,0,0q�6'   �� 5       � ��$ 15(�6)   5      5      .��5      � 
������� � ColumnDefinitions 5   
    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��20q�6"   .+�5      �� ��6   �� *Sport+�� 5      ��6   �� *	LapName+�� 5      ��6   �� *
Distance+�� 5      ��6   �� *DateOfEvent+�� 5        �� Orientation$ 
Horizontal=�6   $��
470,10,0,0q�62   .+�5!      �� $��Validate DTD��6   ��60   �� *validateDTD+�� 5"      $��Parse the list to XML��6   ��69   �� *	runParser+5#      5$      �� 5%   
   $��10,10,10,10q�64   $��1��6I   ��6   �� *
Cyclists+��5&      �� 5'       �� DataType"   WPFMVVMStarter.Model.Cyclist"  6   5)       �� View 5+      �� 5,      .P�5-      �� 	 �� Width$		 200��6%   
 �� DisplayMemberBinding
 61   �� *Name+��5.      �� 5/      ��63   �� *
sortbyName+.��6R   Name6X   51      �� 52      $��Gender��6%   $		 100��65   
 6A   �� *Gender+�� 53      $		 100��6%   
 61   �� *CountryOrigin+��54      �� 55      ��63   �� *sortbyCountry+.��6U   	CountryId6`   57      �� 58      $		 100��6%   
 61   �� *
ResultTime+��59      �� 5:      ��63   �� *sortbyEndPosition+.��6Y   Result Time6f   5<      �� 5=      $		 100��6%   
 61   �� *EndPosition+��5>      �� 5?      ��63   �� *sortbyEndPosition+.��6Y   End Position6g   5A      5B      5D      5E      5F      