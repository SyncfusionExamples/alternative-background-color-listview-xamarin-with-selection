# How to set background color alternatively in Xamarin.Forms ListView (SfListView) ?

You can change the [BackgroundColor](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.visualelement.backgroundcolor?view=xamarin-forms) of [ItemTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemTemplate.html?) loaded in the Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview?) based on the value changed in [Trigger](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/triggers) with consideration of **Selection**.

You can also refer the following article.

https://www.syncfusion.com/kb/11295/how-to-set-background-color-alternatively-in-xamarin-forms-listview-sflistview 

**XAML**

Defined **Trigger** for the parent element of SfListView ItemTemplate and bind the model class property to change the Background color of the item.

``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:RowStyle"
             xmlns:listView="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="RowStyle.MainPage">
 
    <ContentPage.BindingContext>
        <local:ContactsViewModel x:Name="viewModel"/>
    </ContentPage.BindingContext>
 
    <ContentPage.Resources>
        <ResourceDictionary>
            <local:IndexToColorConverter x:Key="IndexToColorConverter"/>
        </ResourceDictionary>
    </ContentPage.Resources>
    
    <ContentPage.Content>
        <listView:SfListView x:Name="listView" ItemsSource="{Binding Items}" ItemSize="50" SelectionMode="SingleDeselect" 
                             SelectionChanging="ListView_SelectionChanging" SelectionBackgroundColor="Beige">
            <listView:SfListView.ItemTemplate>
                <DataTemplate>
                    <Grid Padding="10,0,0,0"  x:Name="grid">
                        <Grid.Triggers>
                            <DataTrigger TargetType="Grid" Binding="{Binding Source={x:Reference grid},
                                       Path=BindingContext.IsSelected}" Value="False">
                                <Setter Property="BackgroundColor" Value="{Binding ., Converter={StaticResource IndexToColorConverter},ConverterParameter={x:Reference listView}}" />
                            </DataTrigger>
                            <DataTrigger TargetType="Grid" Binding="{Binding Source={x:Reference grid},
                                       Path=BindingContext.IsSelected}" Value="True">
                                <Setter Property="BackgroundColor" Value="SlateBlue" />
                            </DataTrigger>
                        </Grid.Triggers>
                        <Grid.RowDefinitions>
                            <RowDefinition Height="*" />
                            <RowDefinition Height="*" />
                        </Grid.RowDefinitions>
                        <Label LineBreakMode="NoWrap" 
                   VerticalTextAlignment="End"
                   Text="{Binding ContactName}"/>
                        <Label Grid.Row="1"
                   VerticalTextAlignment="Start"
                   Text="{Binding ContactNumber}"/>
                    </Grid>
                </DataTemplate>
            </listView:SfListView.ItemTemplate>
        </listView:SfListView>
    </ContentPage.Content>
</ContentPage>
```
**C#**

Create model class property to indicate whether the item is selected or not. The background color will be updated based on the property value.

``` c#
namespace ListViewXamarin
{
    public class Contacts : INotifyPropertyChanged
    {
        private bool isSelected;
 
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    this.RaisedOnPropertyChanged("IsSelected");
                }
            }
        }
    }
}
```
**C#**

Changing the **IsSelected** property in the SelectionChanging event of ListView.
``` c#
ListView.SelectionChanging += ListView_SelectionChanging;
 
private void ListView_SelectionChanging(object sender, ItemSelectionChangingEventArgs e)
{
    for (int i = 0; i < e.AddedItems.Count; i++)
    {
        var item = e.AddedItems[i] as Contacts;
        item.IsSelected = true;
    }
 
    for (int i = 0; i < e.RemovedItems.Count; i++)
    {
        var item = e.RemovedItems[i] as Contacts;
        item.IsSelected = false;
    }
}
```
**C#**

[Coverter](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/data-binding/converters) to apply the alternate row style based on the index value of items.

``` c#
namespace ListViewXamarin 
{
    public class IndexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var listview = parameter as SfListView;
            return listview.DataSource.DisplayItems.IndexOf(value) % 2 == 0 ? Color.LightGray : Color.Aquamarine;
        }
 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
```
**Output**

![AlternateBackgroundSelection](https://github.com/SyncfusionExamples/alternative-background-color-listview-xamarin-with-selection/blob/master/ScreenShot/alternatebackground.jpg)
