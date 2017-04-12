<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BacWebScan.aspx.vb" Inherits="BacWebScan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="Resources/dynamsoft.webtwain.initiate.js"> </script>
    <script type="text/javascript" src="Resources/dynamsoft.webtwain.config.js"> </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 366px" align="left">
        <div id="dwtcontrolContainer"> </div>
        &nbsp;
        <script type="text/javascript"> 
        function AcquireImage(){
            var DWObject = Dynamsoft.WebTwainEnv.GetWebTwain('dwtcontrolContainer');
            DWObject.IfDisableSourceAfterAcquire = true;
            DWObject.SelectSource();
            DWObject.OpenSource();
            DWObject.AcquireImage();
        }
        
        function OnSuccess() {
            console.log('successful');
        }
        function OnFailure(errorCode, errorString) {
            alert(errorString);
        }    
            
        function SaveWithFileDialog() {
            alert("Inside Save");
            var DWObject = Dynamsoft.WebTwainEnv.GetWebTwain('dwtcontrolContainer');
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    alert(DWObject.HowManyImagesInBuffer);
                    DWObject.IfShowFileDialog = true;
//                        DWObject.SaveAsJPEG("C:\\DynamicWebTWAIN.jpg", DWObject.CurrentImageIndexInBuffer);			
                        DWObject.SaveAllAsPDF("C:\\DynamicWebTWAIN.pdf", OnSuccess, OnFailure);                        

//                    if (document.getElementById("imgTypejpeg").checked == true) {
//				        //If the current image is B&W
//                        //1 is B&W, 8 is Gray, 24 is RGB
//                        if (DWObject.GetImageBitDepth(DWObject.CurrentImageIndexInBuffer) == 1)
//                            //If so, convert the image to Gray
//                            DWObject.ConvertToGrayScale(DWObject.CurrentImageIndexInBuffer);
//                        //Save image in JPEG
//                        DWObject.SaveAsJPEG("C:\\DynamicWebTWAIN.jpg", DWObject.CurrentImageIndexInBuffer);			
//			        }
//                    else if (document.getElementById("imgTypetiff").checked == true) 
//                        DWObject.SaveAllAsMultiPageTIFF("C:\\DynamicWebTWAIN.tiff", OnSuccess, OnFailure);
//                    else if (document.getElementById("imgTypepdf").checked == true) 
//                        DWObject.SaveAllAsPDF("C:\\DynamicWebTWAIN.pdf", OnSuccess, OnFailure);
                }
            }
        }           
        
        function OnHttpUploadSuccess() {
            console.log('successful');
        }
        function OnHttpUploadFailure(errorCode, errorString, sHttpResponse) {
            alert(errorString + sHttpResponse);
        }
        
        function UploadImage() {
        var DWObject = Dynamsoft.WebTwainEnv.GetWebTwain('dwtcontrolContainer');
        if (DWObject) {
            // If no image in buffer, return the function
            if (DWObject.HowManyImagesInBuffer == 0)
                return;
     
            alert("Inside Server Save 1");
            var strHTTPServer = location.hostname;
            var CurrentPathName = unescape(location.pathname);
            var CurrentPath = CurrentPathName.substring(0, CurrentPathName.lastIndexOf("/") + 1);
            var strActionPage = CurrentPath + "SaveToFile.aspx";
            alert (strActionPage);
            DWObject.IfSSL = false; // Set whether SSL is used
            DWObject.HTTPPort = location.port == "" ? 80 : location.port;
            var Digital = new Date();
            var uploadfilename = Digital.getMilliseconds();
            DWObject.HTTPUploadAllThroughPostAsPDF(strHTTPServer, strActionPage, uploadfilename + ".pdf", OnHttpUploadSuccess, OnHttpUploadFailure);
            alert("Successfull");
        }
    }                                   
                                         
        
        </script><asp:Label ID="lblSelectDocType" runat="server" Font-Bold="True" 
            Font-Names="Verdana" Text="Document Type" Font-Size="Medium"></asp:Label>
        <asp:DropDownList ID="ddlDocType" runat="server" CausesValidation="True" 
            DataTextFormatString="Select Document Type" Font-Bold="True" 
            Font-Names="veranda" Height="39px" Width="334px">
        </asp:DropDownList>
        <asp:Label ID="lblDocType" runat="server" Font-Bold="True" Font-Names="veranda" 
            ForeColor="#FF9900" Text="No Document Type Selected" Font-Size="Medium"></asp:Label>
        <br />
        <input type="button" value="Scan" onclick="AcquireImage();" />    &nbsp;
        <input type="button" value="Save" onclick="UploadImage();"  /> &nbsp;
            
        <asp:ImageButton ID="btnRight" runat="server" Height="16px" ToolTip="Right" 
            Width="61px" />    &nbsp;
            
        <asp:ImageButton ID="btnLeft" runat="server" Height="21px" ToolTip="Left" 
            Width="72px" />    &nbsp;
            
        <asp:ImageButton ID="btnDelete" runat="server" Height="17px" Width="84px" />    &nbsp;
        
        <asp:ImageButton ID="btnDeleteAll" runat="server" Height="19px" Width="90px" />    &nbsp;
        
        <asp:ImageButton ID="btnExit" runat="server" Height="19px" Width="77px" />    &nbsp;
        
        <asp:CheckBox ID="CheckBox1" runat="server" />
            &nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="veranda" 
            Font-Size="Small" Height="28px" style="margin-left: 0px" Text="EnableDuplex?" 
            Width="60px"></asp:Label>    &nbsp;
            
        <asp:Image ID="pbBrightness" runat="server" Height="33px" Width="34px" />    &nbsp;
        
        <asp:Image ID="pbContrast" runat="server" Height="33px" ImageAlign="Bottom" 
            Width="34px" />
    </div>
    </form>
</body>
</html>
