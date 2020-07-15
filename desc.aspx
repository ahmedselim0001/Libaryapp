<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="desc.aspx.cs" Inherits="APLiberary.desc" %>

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

  <title>AP Library</title>

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
          <li class="nav-item active">
 <asp:HyperLink id="HyperLink2" 
                  class="nav-link"
                  NavigateUrl="home.aspx"
                  Text="Home"
                  runat="server"/>               <span class="sr-only">(current)</span>
          </li>
                <li class="nav-item">
               <asp:HyperLink id="fav" 
                  class="nav-link"
                  NavigateUrl="fav.aspx"
                  Text="Favorites"
                  runat="server"/> 
          </li>

            <li class="nav-item">
               <asp:HyperLink id="userorders" 
                  class="nav-link"
                  NavigateUrl="user-orders.aspx"
                  Text="Your orders"
                  runat="server"/> 

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

    	<div class="bg-contact2" style="background-image: url('images/bg-01.jpg');">
		<div class="container-contact2">
			<div class="wrap-contact2">
				<form class="contact2-form validate-form" runat="server">
                                <asp:HiddenField ID="HiddenField1" runat="server" />

                   <div class="wrap-input2 validate-input" data-validate = "Valid email is required: ex@abc.xyz" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label runat="server" ID="title" 
					 Font-Bold="True"  Font-Size="X-Large" ></asp:Label > </div>
				
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:Image runat="server" ID="disimage" BorderStyle="Solid" Height="258px" ImageAlign="Middle" Width="352px"/>
					<div class="container-contact2-form-btn">
						<div class="wrap-contact2-form-btn">
                            <asp:Label runat="server" ID="auther" Text="Auther: "></asp:Label>
                            <br />
                                                        <asp:Label runat="server" ID="location" Text="Location: "></asp:Label>
                            <br />
                            <asp:Label runat="server" ID="sn"></asp:Label>
                            <br />
                                                        <asp:Label runat="server" ID="descr" Text="Descreption"></asp:Label>
                                <br />
                            <asp:Label runat="server" ID="quan" Text="Avilable Copies: "></asp:Label>
                            <br />
                            <asp:Label runat="server" ID="price" Text="Price: "></asp:Label>
                                <br />
                                                      <asp:Label runat="server" ID="Label1" Text="Read the Book"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="bttnpdf" runat="server" Text="Open PDF" Font-Bold="True" OnClick="bttnpdf_Click" Height="31px" Width="208px" />
                            <br />

                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            
                            
                            <asp:Button runat="server" ID="add" BackColor="#FF3300" ForeColor="White" Height="52px" Width="195px"  Text="Add to favourite" OnClick="add_Click"/>
                            &nbsp;<asp:Button runat="server" ID="comments" BackColor="#FF3300" ForeColor="White" Height="52px" Width="195px"  Text="comments" OnClick="comments_Click"/>
                            &nbsp;<asp:Button runat="server" ID="order" BackColor="#FF3300" ForeColor="White" Height="52px" Width="195px"  Text="Order it" OnClick="order_Click"/>
                            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label runat="server" ID="succ"></asp:Label>   
						</div>
					</div>


                    <br />
                    <div class="">
           
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
