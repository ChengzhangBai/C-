<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterCourse.aspx.cs" Inherits="Lab8.RegisterCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register courses</title>
    <link href="App_Themes/SiteStyles.css" rel="stylesheet" />
</head>
<body>
    <h1> Registrations</h1>
    <form id="form1" runat="server">
        Student: 
        <asp:DropDownList ID="stuLst" runat="server"  AutoPostBack="true" OnTextChanged="nameChanged">
            <asp:ListItem Value="">Select... </asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="stuLst" ErrorMessage="Must select one!" ForeColor="Red" Visible="True"></asp:RequiredFieldValidator>
    <br />  <br /> 
        <asp:Label ID="Label2" runat="server" ForeColor="Blue"></asp:Label>
        <br />
    <asp:Label ID="lblChoose" runat="server">Following courses are currently available for registration</asp:Label> 
        <br /><asp:Label ID="errMsg" runat="server" cssClass="error"> </asp:Label>
        <asp:CheckBoxList ID="chkboxCourseLst" runat="server" AutoPostBack="true" OnSelectedIndexChanged="coursesLstChg">      
        </asp:CheckBoxList>
        <br /><asp:Button ID="btnSubmit" runat="server" OnClick="btnRegisterCourses" Text="Save" />
        
        <br /> <br />
        <a href="AddStudent.aspx"> Add Students</a></form>
</body>
</html>
