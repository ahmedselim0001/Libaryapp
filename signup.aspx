<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="APLiberary.signup" %>
<%@ Register Assembly="BotDetect" Namespace="BotDetect.Web.UI"
    TagPrefix="BotDetect" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
<!--===============================================================================================-->
	<link rel="icon" type="image/png" href="images/icons/favicon.ico"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/animate/animate.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/css-hamburgers/hamburgers.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="css/util.css">
	<link rel="stylesheet" type="text/css" href="css/main.css">
<!--===============================================================================================-->
<meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">

  <title>APL-Admin</title>

  <!-- Bootstrap core CSS -->
  <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

  <!-- Custom styles for this template -->
  <link href="css/shop-item.css" rel="stylesheet"></head>
<body>
    <!-- Navigation -->
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">
    <div class="container">
  <asp:HyperLink id="navba" 
                  class="navbar-brand"
                  NavigateUrl="home.aspx"
                  Text="AP Liberary"
                  runat="server"/>     
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarResponsive">
        <ul class="navbar-nav ml-auto">
          <li class="nav-item ">
<asp:HyperLink id="HyperLink3" 
                  class="nav-link"
                  NavigateUrl="home.aspx"
                  Text="Home"
                  runat="server"/>              
              <span class="sr-only">(current)</span>
          </li>
          <li class="nav-item active">
             <asp:HyperLink id="HyperLink2" 
                  class="nav-link"
                  NavigateUrl="reg.aspx"
                  Text="Register new admin"
                  runat="server"/> </li>


          <li class="nav-item">
              <asp:Label ID="ID"  class="nav-link" runat="server" Text=""></asp:Label>
          </li>
          <li class="nav-item">
               <asp:HyperLink id="hyperlink1" 
                  class="nav-link"
                  NavigateUrl="Login.aspx"
                  Text="Logout"
                  runat="server"/> 
          </li>
        </ul>
      </div>
    </div>
  </nav>

  <!-- Page Content -->
\    	<div class="bg-contact2" style="background-image: url('images/bg-01.jpg');">
		<div class="container-contact2">
			<div class="wrap-contact2">
				<form class="contact2-form validate-form" runat="server">
                                <asp:HiddenField ID="HiddenField1" runat="server" />

                 
                    
                                        <br />

                    	<div class="wrap-input2 validate-input" data-validate="Title is required">
               <asp:TextBox ID="name"  cssclass="input2"  runat="server" required="required" placeholder="Name"></asp:TextBox>
                         </div>

                        <div class="wrap-input2 validate-input" data-validate = "Location is required">
                    <asp:TextBox placeholder="Email" ID="email" runat="server" class="input2" name="name" required="required" TextMode="Email"></asp:TextBox>
                            	</div>
                    
                    <asp:Label ID="lblsc2" runat="server" Text="" ForeColor="red"></asp:Label>
                    <div class="wrap-input2 validate-input" data-validate = "Valid email is required: ex@abc.xyz">
                        <asp:TextBox placeholder="Password" ID="pass" runat="server" class="input2" type="text" name="email" TextMode="Password" required="required"></asp:TextBox>
									</div>

					<div class="wrap-input2 validate-input" data-validate = "Auther is required">
                    <asp:TextBox placeholder="Repeat Password" ID="pass2" runat="server" class="input2" name="name" required="required" TextMode="Password"></asp:TextBox>
                       
                        </div>

                    <botdetect:captcha ID="captchaBox" runat="server"></botdetect:captcha>
                    <div class="wrap-input2 validate-input" data-validate = "Auther is required">
                    <asp:TextBox placeholder="Captcha Code" ID="txtCaptcha" runat="server" class="input2" name="name" required="required" ></asp:TextBox>
                        </div>
                    
                    <asp:Label ID="lblsc" runat="server" Text="" ForeColor="red"></asp:Label>


					<div class="container-contact2-form-btn">
						<div class="wrap-contact2-form-btn">
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;




  <asp:Button ID="btnsave" runat="server" Text="Sign Up" OnClick="btnsave_Click" BackColor="#FF3300" ForeColor="White" Height="52px" Width="195px" />
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						</div>
					</div>

				</form>
              

			</div>

		</div>

	</div>




<!--===============================================================================================-->
	<script src="vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
	<script src="vendor/bootstrap/js/popper.js"></script>
	<script src="vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
	<script src="vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
	<script src="js/main.js"></script>

	<!-- Global site tag (gtag.js) - Google Analytics -->
	<script async src="https://www.googletagmanager.com/gtag/js?id=UA-23581568-13"></script>
	<script>
	  window.dataLayer = window.dataLayer || [];
	  function gtag(){dataLayer.push(arguments);}
	  gtag('js', new Date());

	  gtag('config', 'UA-23581568-13');
	</script>









      
  <!-- /.container -->

  <!-- Footer -->
  <footer class="py-5 bg-dark">
    <div class="container">
      <p class="m-0 text-center text-white">Copyright &copy; 2019 Asia Pacific University of Technology & Innovation (APU). All rights reserved.</p>
    </div>
    <!-- /.container -->
  </footer>

  <!-- Bootstrap core JavaScript -->
  <script src="vendor/jquery/jquery.min.js"></script>
  <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

</body>
</html>