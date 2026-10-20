<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

        .register-page {
            width: 450px;
            margin: 50px auto;
            background-color: white;
            padding: 35px;
            border-radius: 10px;
            box-shadow: 0 2px 10px #ddd;
        }

        .register-page h1 {
            text-align: center;
            color: #0b3d91;
            margin-bottom: 30px;
        }

        .form-row {
            margin-bottom: 20px;
        }

        .form-row label {
            display: block;
            font-weight: bold;
            margin-bottom: 8px;
        }

        .input-box {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .register-btn {
            width: 100%;
            padding: 13px;
            background-color: #0b3d91;
            color: white;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-size: 16px;
        }

        .register-btn:hover {
            background-color: #ffcc00;
            color: black;
        }

        .message {
            display: block;
            margin-top: 15px;
            text-align: center;
        }

    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

    <div class="register-page">

        <h1>Create Account</h1>

        <div class="form-row">
            <label>Full Name</label>

            <asp:TextBox ID="txtName"
                runat="server"
                CssClass="input-box">
            </asp:TextBox>
        </div>


        <div class="form-row">
            <label>Email</label>

            <asp:TextBox ID="txtEmail"
                runat="server"
                CssClass="input-box">
            </asp:TextBox>
        </div>


        <div class="form-row">
            <label>Phone</label>

            <asp:TextBox ID="txtPhone"
                runat="server"
                CssClass="input-box"
                MaxLength="10">
            </asp:TextBox>
        </div>


        <div class="form-row">
            <label>Password</label>

            <asp:TextBox ID="txtPassword"
                runat="server"
                CssClass="input-box"
                TextMode="Password">
            </asp:TextBox>
        </div>


        <asp:Button ID="Button1"
            runat="server"
            Text="Register"
            CssClass="register-btn"
            OnClick="Button1_Click" />


        <asp:Label ID="Label1"
            runat="server"
            CssClass="message">
        </asp:Label>

    </div>

</asp:Content>