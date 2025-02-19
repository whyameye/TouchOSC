class "TouchArea (TouochArea.cs)" is what captures the touches
  - red box defines the touch area
  - constructor has this.opacity to set opacity

set opacity of touches in MainWindow.xaml.cs:
  - instance of TableControl has property Opacity
  - this also affects opacity of red box

set opacity of primary window in MainWindow.xaml
  - Background 1st 2 hex digits = opacity. Lower is more transparent
  - opacity of 00 creates click-through so use 01 as lowest setting

remove/add cursor in "MainWindow.xaml.cs" (win10 only?)
  - this.Cursor = Cursors.None (comment or uncomment)