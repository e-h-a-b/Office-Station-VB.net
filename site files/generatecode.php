<?php

include_once 'dbconnect.php';

function rand_code($len)
{
 $min_lenght= 0;
 $max_lenght = 100;
 $bigL = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
 $smallL = "abcdefghijklmnopqrstuvwxyz";
 $number = "0123456789";
 $bigB = str_shuffle($bigL);
 $smallS = str_shuffle($smallL);
 $numberS = str_shuffle($number);
 $subA = substr($bigB,0,5);
 $subB = substr($bigB,6,5);
 $subC = substr($bigB,10,5);
 $subD = substr($smallS,0,5);
 $subE = substr($smallS,6,5);
 $subF = substr($smallS,10,5);
 $subG = substr($numberS,0,5);
 $subH = substr($numberS,6,5);
 $subI = substr($numberS,10,5);
 $RandCode1 = str_shuffle($subA.$subD.$subB.$subF.$subC.$subE);
 $RandCode2 = str_shuffle($RandCode1);
 $RandCode = $RandCode1.$RandCode2;
 if ($len>$min_lenght && $len<$max_lenght)
 {
 $CodeEX = substr($RandCode,0,$len);
 }
 else
 {
 $CodeEX = $RandCode;
 }
 return $CodeEX;
}
?>


<?php
$i = 1;
while ($i <= 1000000):
$gnr=rand_code(16);			
//   $sqml = "SELECT * FROM `id2748680_bank`.`Coin`  where Con='$gnr' and  id='$id'";
 //    $roms=$conn->query($sqml);
 //    if($roms->num_rows>0){
   //  while($rw=$roms->fetch_assoc()){

         // }}
		//else
		//{
		    
$dn2 = mysqli_query($conn,"SELECT MAX(id) FROM `id2748680_bank`.`Coin`");
	$id = $dn2;

			$id = $id+1;
mysqli_query($conn,"INSERT INTO `id2748680_bank`.`Coin` (`Con`,`id`,`date`,`price`) VALUES ('$gnr','$i','0','0')");

	echo '</br>';
echo $i;
    $i++;

		//}
		endwhile;		


?>