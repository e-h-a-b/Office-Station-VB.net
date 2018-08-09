<?php
 
//$host = "mysql.hostinger.in";
//$user = "********";
//$pass = "********";
//$data = "********";
 ob_start();
	session_start();
	require_once 'dbconnect.php'; 
//$host = "localhost";
//$user = "********";
//$pass = "********";
//$databasename = "********";
//$host = "mysql.hostinger.in";
//$user = "********";
//$pass = "********";
//$databasename = "***********";

 //try
 //{
	//$con=new PDO("mysql:host=$host:port=8888:dbname=id2748680_bank",$user,$pass);
    //$con->setattribute(PDO::	ATTE_ERRMODE,PDO::ERRMODE_EXCEPTION);

 //}
//catch(Exception $e){
//echo "connection error : ".$e->getmessage();	
	 
	 
	 
 //}
 
 
   //      'host'->env('DB_HOST', 'localhost');
     //   'database'->env('DB_DATABASE', '*****************');
       // 'username'->env('DB_USERNAME', '*****************');
        //'password'->env('DB_PASSWORD', '*****************');
      
 
 
 
$date = date('Y-m-d h:i:s A');
//$con = new mysqli($host, $user, $pass, $data);
 
$action = false;
$mac = false;
$code = false;
 
$action =  $conn->real_escape_string($_GET['action']);
   
  
 
$mac =  $conn->real_escape_string($_GET['mac']);
$code =  $conn->real_escape_string($_GET['code']);

   $Stpasss = $_GET['Stpasss'];
   
 	$StId =  $_GET['StId'];
 $StName = $_GET['StName'];
 $Stdatet = date('Y-m-d h:i:s A');
 $Stprogram = $_GET['Stprogram'];
 $Stamount = $_GET['Stamount'];
 $Sttimesent = $_GET['Sttimesent'];
 $Stprice = $_GET['Stprice'];
 $Stpaym = $_GET['Stpaym'];
 $Ststatupay = $_GET['Ststatupay'];
 $Ststatu = $_GET['Ststatu'];
 
  $Stmobile = $_GET['Stmobile'];
  $Stfacebook = $_GET['Stfacebook'];
 
if(!$action)
{
	echo "Please enter an action.";
}
else
{	
	if($action == "Accept")
	{
		if($query = $con->query("INSERT INTO acep (accept,Decliend,mac,time) VALUES ('1','0','$mac','$date')"))
		{
			echo "1";
		}
		else
		{
			echo "0";
		}
	}
	else if($action == "Decliend")
	{
		if($query = $con->query("INSERT INTO acep (accept,Decliend,mac,time) VALUES ('0','1','$mac','$date')"))
		{
			echo "1";
		}
		else
		{
			echo "0";
		}
		
		
		}
	 else if($action =="NumAccept")
	{
		$query = $con->query("SELECT Accept FROM acep WHERE Accept='1' and Decliend='0'");
		$cnt = $query->num_rows;
		
		if($cnt > 0)
		{
				 
        echo $cnt;

        } 
		 
		 
		else
		{
			echo "0";
		}
		 
	 }
	else if ($action =="NumDecliend")
	{
		
	    $query = $con->query("SELECT Decliend FROM acep WHERE Decliend='1' and Accept='0'");
		$cnt = $query->num_rows;
		
		if($cnt > 0)
		{
				 
        echo $cnt;

        }
 
		
		 
		else
		{
			echo "0";
		}		
	}
	else if ($action =="check")
	{
		
	   $query = $con->query("SELECT mac FROM acep WHERE mac = '$mac'");
		$cnt = $query->num_rows;
		
		if($cnt > 0)
		{
			
			echo "stop";
		
		}
		else
		{
			echo "0";
		}
		
	}
	else if ($action =="yanasb")
	{
		//$dn2 = mysql_num_rows(mysql_query('select id from yanasb'));
		//$id = $dn2+1;
	  if( mysqli_query($con ,"INSERT INTO yanasb (id,code,winNum,mac,time) VALUES ('1','$code','','','')"))
		{
			echo "1";
		}
		else
		{
			echo "0";
		}
		
	}
			else if ($action =="setmac")
	  {
		 
	   $query = $con->query("SELECT code FROM yanasb WHERE code = '$code'");
		$cnt = $query->num_rows;
		
		if($cnt > 0)
		{
			$query = $con->query("INSERT INTO yanasb (mac,time) VALUES ('$mac','$date')");
			echo "stop";
		
		}
		else
		{
			echo "0";
		}
		
	}
		else if ($action =="read")
	{
		
	    $sqml = "SELECT code FROM yanasb WHERE mac=''";
$roms=$con->query($sqml);
if($roms->num_rows>0){
while($rw=$roms->fetch_assoc()){
    echo $rw['code'];
	echo '####';
}}
		
 
		
		 
		else
		{
			echo "0";
		}		
	}
	//Station

	else if($action =="Set")
	{
		$bg = $conn->query("SELECT COUNT(*) FROM `Station`");
    $rowmm = $bg->fetch_row();
	$muum=$rowmm[0]+1;
		if(mysqli_query($conn,"INSERT INTO `id2748680_bank`.`Station` (`Id`,`Idw`,`datet`,`program`,`amount`,`timesent`,`price`,`paym`,`statupay`,`statu`,`facebook`) VALUES ('$StId','$muum','$Stdatet','$Stprogram','$Stamount','$Sttimesent','$Stprice','$Stpaym','$Ststatupay','$Ststatu','$Stfacebook')"))
	 
		{
			
 	echo "1";
			 
		}
		else
		{
			echo "0";
		}
		
		
		}
		else if ($action =="perv")
	{
		
	    $sqml = "SELECT * FROM Station ";
$roms=$conn->query($sqml);
if($roms->num_rows>0){
while($rw=$roms->fetch_assoc()){
    echo  $rw['Idw'].','.$rw['datet'].','.$rw['program'].','.$rw['amount'].','.$rw['timesent'].','.$rw['price'].','.$rw['paym'].','.$rw['statupay'].','.$rw['statu'];
	echo '####';
}}
		
 
		
		 
		else
		{
			echo "0";
		}		
	}
		else if ($action =="perve")
	{
		if($sqml = "SELECT * FROM Stationuser where ID='$StId' and Password='$Stpasss'")
		{
	    $sqml = "SELECT * FROM Station where Id='$StId'";
$roms=$conn->query($sqml);
if($roms->num_rows>0){
while($rw=$roms->fetch_assoc()){
    echo  $rw['Idw'].','.$rw['datet'].','.$rw['program'].','.$rw['amount'].','.$rw['timesent'].','.$rw['price'].','.$rw['paym'].','.$rw['statupay'].','.$rw['statu'];
	echo '####';
}}
			
 }
		
		 
		else
		{
			echo "0";
		}		
	}
		else if ($action =="signin")
	{
		  $query = $conn->query("SELECT Id FROM Stationuser WHERE Id = '$StId' or facebook='$Stfacebook'");
		$cnt = $query->num_rows;
		
		if($cnt > 0)
		{
			
			echo "stop";
		
		}
		else
		{
			 if(mysqli_query($conn,"INSERT INTO `id2748680_bank`.`Stationuser` (`Id`,`Name`,`mobile`,`Password`,`facebook`) VALUES ('$StId','$StName','$Stmobile','$Stpasss','$Stfacebook')"))
	 
		{
			echo "1";
			 
		}
		else
		{
			echo "0";
		} 
		}
	   
		
	}
		else if ($action =="login")
	{
		
	   if($sqml = "SELECT ID,Password FROM Stationuser where ID='$StId' and Password='$Stpasss'")
		{
				    
$roms=$conn->query($sqml);
if($roms->num_rows>0){
	echo "1";
 }
		else
		{
			echo "0";
		}	
			 
		}
		else
		{
			echo "0";
		}	
		
		
	}
		else if ($action =="name")
	{
		
	    $sqml = "SELECT * FROM Stationuser  where ID='$StId' and Password='$Stpasss'";
$roms=$conn->query($sqml);
if($roms->num_rows>0){
while($rw=$roms->fetch_assoc()){
    echo  $rw['Name'] ;
	 
}}
		
 
		
		 
		else
		{
			echo "0";
		}		
	}
		else if ($action =="mobile")
	{
		
	    $sqml = "SELECT * FROM Stationuser  where ID='$StId' and Password='$Stpasss'";
$roms=$conn->query($sqml);
if($roms->num_rows>0){
while($rw=$roms->fetch_assoc()){
    echo  $rw['mobile'] ;
	 
}}
		
 
		
		 
		else
		{
			echo "0";
		}		
	}
			else if ($action =="Facebookpro")
	{
		
	   $sqml = "SELECT * FROM Station  where  Idw='$StName'";
$roms=$conn->query($sqml);
if($roms->num_rows>0){
while($rw=$roms->fetch_assoc()){
    echo  $rw['facebook'] ;
	 
}}
		
 
		
		 
		else
		{
			echo "0";
		}		
	}
		else if ($action =="Facebook")
	{
		
	 $sqml = "SELECT * FROM Stationuser  where ID='$StId' and  password='$Stpasss'";
     $roms=$conn->query($sqml);
     if($roms->num_rows>0){
     while($rw=$roms->fetch_assoc()){
        echo  $rw['facebook'] ;
          }}
		else
		{
			echo "0";
		}		
	}
		else if ($action =="pro")
	{
		
	    $sqml = "SELECT * FROM Station  where Idw='$StName'";
$roms=$conn->query($sqml);
if($roms->num_rows>0){
while($rw=$roms->fetch_assoc()){
    echo  $rw['Id'].','.$rw['Idw'].','.$rw['datet'].','.$rw['program'].','.$rw['amount'].','.$rw['timesent'].','.$rw['price'].','.$rw['paym'].','.$rw['statupay'].','.$rw['statu'] ;
	 
}}
		
 
		
		 
		else
		{
			echo "0";
		}		
	}
       else if($action =="Delet")
	{
		$sqml = "DELETE FROM Station WHERE Idw='$StName' and Id='$StId'";
		$roms=$conn->query($sqml);
if($roms->num_rows=0){
while($rw=$roms->fetch_assoc()){
	 echo "Done";
}}
      else
		{
			echo "0";
		}
	}
	else if ($action =="upd")
	{
	$sqml="UPDATE Station SET statu='$Ststatu' where  Idw='$StName' and Id='$StId'";	
	$roms=$conn->query($sqml);
if($roms->num_rows=0){
	echo "Done";
while($rw=$roms->fetch_assoc()){
	 
}}
      else
		{
			echo "0";
		}
	}
	
	else
	{
		echo "Invalid action.";
	}
}

?>