<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="Lab7.AddStudent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="App_Themes/SiteStyles.css" />
    <title>Student</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>
            Students</h1>
 
           <asp:Table ID="Table1" runat="server">
               <asp:TableRow runat="server">
                   <asp:TableCell runat="server" HorizontalAlign="Justify">Student Name:</asp:TableCell>
                   <asp:TableCell runat="server"><asp:TextBox ID="StudentName" autocomplete="off" runat="server" Width="130px"></asp:TextBox>
</asp:TableCell>
                   <asp:TableCell runat="server"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="StudentName" ErrorMessage="Required!" Visible="True" ForeColor="Red"></asp:RequiredFieldValidator></asp:TableCell>
               </asp:TableRow>
               <asp:TableRow runat="server">
                   <asp:TableCell runat="server" HorizontalAlign="Justify">Student Type:</asp:TableCell>
                   <asp:TableCell runat="server"><asp:ListBox ID="StudentType" runat="server" Rows="1" Width="138px">
                <asp:ListItem Value="">Select......</asp:ListItem>
                
<asp:ListItem Value="ft">Full time</asp:ListItem>
                
<asp:ListItem Value="pt">Part time</asp:ListItem>
                
<asp:ListItem Value="co">Coop</asp:ListItem>
            
</asp:ListBox>
</asp:TableCell><asp:TableCell runat="server"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="StudentType" ErrorMessage="Must select one!" Visible="True" ForeColor="Red"></asp:RequiredFieldValidator></asp:TableCell>
               </asp:TableRow>
               <asp:TableRow runat="server">
                   <asp:TableCell runat="server"><asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Add" />
</asp:TableCell>
               </asp:TableRow>
        </asp:Table>
 
                 
        
 
                 
   <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>      

<asp:Table ID="tblStudents" runat="server" CssClass="table">
            <asp:TableRow runat="server">
                <asp:TableHeaderCell runat="server" HorizontalAlign="Center" Width="20px">ID</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" HorizontalAlign="Center">Student Name</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" HorizontalAlign="Center">Type</asp:TableHeaderCell>

        
            </asp:TableRow></asp:Table>
        <br />
        <a href="RegisterCourse.aspx">Register Courses</a></form>
</body>
</html>
