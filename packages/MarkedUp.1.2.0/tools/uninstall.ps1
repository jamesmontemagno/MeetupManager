param($installPath, $toolsPath, $package, $project) 

#get script folder
$scripts = $project.ProjectItems | Where-Object { $_.Name -eq "js" }

if ($scripts) {
	
	#check for existing markedup.js file
	$mk = $scripts.ProjectItems | Where-Object { $_.Name -eq "MarkedUp.js" }

	if ($mk) {
		
		#delete markedup.js from the scripts folder
		$mk.Delete()

	}

}