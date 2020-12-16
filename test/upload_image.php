<?php
$file = $_FILES["image"]['tmp_name'];
$fileName = "user_image_" . ip2long($_SERVER["REMOTE_ADDR"]) . ".png";
move_uploaded_file($file, $fileName); ?>