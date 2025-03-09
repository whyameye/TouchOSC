# Code Notes

## class "TouchArea (TouchArea.cs)" is what captures the touches
  - red box defines the touch area
  - constructor has this.opacity to set opacity

## set opacity of touches in MainWindow.xaml.cs:
  - instance of TableControl has property Opacity
  - this also affects opacity of red box

## set opacity of primary window in MainWindow.xaml
  - Background 1st 2 hex digits = opacity. Lower is more transparent
  - opacity of 00 creates click-through so use 01 as lowest setting

## remove/add cursor in "MainWindow.xaml.cs" (win10 only?)
  - this.Cursor = Cursors.None (comment or uncomment)

---
# TUIO Protocol subset i.e. as it pertains to touch table code only
  - Protocol format: `/tuio/2Dcur <msg> <parameter list separated by spaces`
  - each blob gets a unique ID (incremented from last blob's ID)

## TUIO messages
*There are 3 classes of messages sent by the TUIO protocol as it pertains to this code*
## set
  - format: `/tuio/2Dcur set blob_id x_pos y_pos x_vel y_vel m_accel`
  - a separate `set` command is sent for each blob
  - example: `/tuio/2Dcur set 5585 0.125776 0.590743 -0.172729 0.019989 0.0115921`
  - we only need to be concerned with the `x_pos` and `y_pos`. The other values are ignored in the code
  - `x_pos` and `y_pos` values are normalized to between 0 and 1

## alive
 - format: `/tuio/2Dcur alive blob_id0 ... blob_idN`
 - example: `/tuio/2Dcur alive 5585 5587`

## seq
 - format: `/tuio/2Dcur fseq incrementing_frame_id`
 - *this appears to be used in the code for timing so is required*

## execution
*following what appears to be the model from CCV 1.x back in the day from looking at sample data*
 - send data at framerate
 - for each frame:
	- send any set messages
	- send alive message
	- send seq message
- no need to send a set message on touchdown

# TouchTable Pd notes
 - the code already scales everything to aspect ratio of 640/360 regardless of resolution
 - this C# code does not need to be concerned about resolution or aspect ratio. Just send x and y values between 0 and 1
 - the 1920x1080 resolution will need to be hardcoded in the Pd code at some point. There is no aspect ratio change for this resolution.