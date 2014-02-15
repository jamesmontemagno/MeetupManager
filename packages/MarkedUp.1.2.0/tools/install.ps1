param($installPath, $toolsPath, $package, $project) 

#get script folder
$scripts = $project.ProjectItems | Where-Object { $_.Name -eq "js" }

if ($scripts) {
	
	#check for existing markedup.js file
	$mk = $scripts.ProjectItems | Where-Object { $_.Name -eq "MarkedUp.js" }

	#if (!$mk) {
		
		#copy the markedup.js to the scripts folder
		$scripts.ProjectItems.AddFromFileCopy([System.IO.Path]::Combine($toolsPath, "MarkedUp.js"))

	#} else {
		
		#file exists alert
	#	Write-Warning "Skipping adding MarkedUp.js file to project as file with same name already exists"

	#}

}

#get the WMAppManifest.xml file (for Windows Phone)
$properties = $project.ProjectItems | where {$_.Name -eq "Properties"}
if($properties){
	$manifest = $properties.ProjectItems | where {$_.Name -eq "WMAppManifest.xml"}
	if($manifest){
		# find the WMAppManifest.xml path on the file system
		$localPath = $manifest.Properties | where {$_.Name -eq "LocalPath"}

		# load the WMAppManifest.xml as an XML doc
		$xml = New-Object xml
		$xml.Load($localPath.Value)

		#check to see if the ID_CAP_IDENTITY_DEVICE is available
		$idCap = $xml.Deployment.App.Capabilities.Capability | where {$_.Name -eq "ID_CAP_IDENTITY_DEVICE"}
		if(!$idCap){
			#Add the capability if it's not available
			$newNode = $xml.CreateElement("Capability")
			$newNode.SetAttribute("Name", "ID_CAP_IDENTITY_DEVICE")

			$xml.Deployment.App.Capabilities.AppendChild($newNode)

			$xml.Save($localPath.Value)
		}
	}
}