//Maya ASCII 2018 scene
//Name: Mouette.ma
//Last modified: Thu, Jan 09, 2020 12:38:39 PM
//Codeset: 1252
requires maya "2018";
requires -nodeType "VRaySettingsNode" -dataType "VRaySunParams" -dataType "vrayFloatVectorData"
		 -dataType "vrayFloatVectorData" -dataType "vrayIntData" "vrayformaya" "3.60.04";
requires -nodeType "renderSetup" "renderSetup.py" "1.0";
currentUnit -l centimeter -a degree -t ntsc;
fileInfo "application" "maya";
fileInfo "product" "Maya 2018";
fileInfo "version" "2018";
fileInfo "cutIdentifier" "201706261615-f9658c4cfc";
fileInfo "osv" "Microsoft Windows 8 Business Edition, 64-bit  (Build 9200)\n";
fileInfo "license" "education";
fileInfo "vrayBuild" "3.60.04 39f5708";
createNode transform -s -n "persp";
	rename -uid "BAB725DA-4A0A-B921-1988-0BB40B885063";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 1.9896974733391144 5.478403963068069 8.2629016951137224 ;
	setAttr ".r" -type "double3" 328.46164732011272 372.99999999945481 4.0802704183004241e-16 ;
createNode camera -s -n "perspShape" -p "persp";
	rename -uid "A27BA2F0-4802-7472-059A-2EB9949AAA1E";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".coi" 10.760108537811735;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".hc" -type "string" "viewSet -p %camera";
createNode transform -s -n "top";
	rename -uid "C5A90771-44B3-9596-D329-A187863B598A";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 1000.1 0 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	rename -uid "6514C0EB-492F-6EF7-AE50-B3A5D692E051";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
createNode transform -s -n "front";
	rename -uid "44269567-454E-D0BF-702B-13B4FCA0AD97";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 0 1000.1 ;
createNode camera -s -n "frontShape" -p "front";
	rename -uid "4C6D9DFE-4586-E64F-3C01-4D87F2D177CA";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
createNode transform -s -n "side";
	rename -uid "BF9F7E38-4B4F-3B4C-DC10-5AA0BA878E17";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 1000.1 0 0 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	rename -uid "39DE45F7-4ED8-C22D-5175-50A71BC03F31";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
createNode transform -n "pCube1";
	rename -uid "28011942-459C-3BC0-34BF-3399D5BDEAE7";
	setAttr ".rp" -type "double3" 2.3892050299488203 0.18164191612701089 -0.35373838260992102 ;
	setAttr ".sp" -type "double3" 2.3892050299488203 0.18164191612701089 -0.35373838260992102 ;
createNode mesh -n "polySurfaceShape2" -p "pCube1";
	rename -uid "01188BE3-44D6-9E9E-C335-0E97A6CC4F79";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.50503528118133545 0.5 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 40 ".uvst[0].uvsp[0:39]" -type "float2" 0.625 0 0.625 0.25
		 0.625 0.5 0.625 0.75 0.625 1 0.875 0 0.875 0.25 0.625 0 0.625 0.25 0.625 0 0.625
		 0.25 0.625 0 0.875 0 0.875 0.25 0.625 0.25 0.625 0 0.875 0 0.875 0.25 0.625 0.25
		 0.625 0 0.875 0 0.875 0.25 0.625 0.25 0.50503528 0 0.50503528 1 0.50503528 0.75 0.50503528
		 0.5 0.50503528 0.25 0.50503528 0.25 0.50503528 0.25 0.50503528 0 0.50503528 0 0.50503528
		 0.75 0.50503528 0.5 0.625 0.5 0.625 0.75 0.50503528 0.75 0.50503528 0.5 0.625 0.5
		 0.625 0.75;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 36 ".pt[0:35]" -type "float3"  -0.11864721 0.49423844 0.086380377 
		-0.11864721 -0.063161373 0.086380377 -0.11864721 -0.079739109 -0.086380377 -0.11864721 
		0.50639868 -0.086380377 -0.085940264 0.37446028 0.11510552 -0.085940264 0.0020106372 
		0.11510552 -0.04875524 0.37446028 0.16427119 -0.04875524 0.0020106372 0.16427119 
		-0.16982453 0.31093699 -0.072699688 -0.16982453 0.31093699 0.072699688 -0.16982453 
		0.022381896 -0.072699688 -0.16982453 0.022381896 0.072699688 -0.72280639 0.26966354 
		-0.094421357 -0.72280639 0.26966354 0.049220044 -0.72280639 0.063655309 -0.094421357 
		-0.72280639 0.063655309 0.049220044 -1.4833462 0.24759607 -0.28735098 -1.4043154 
		0.24759607 -0.19548622 -1.4833462 0.085722774 -0.28735098 -1.4043154 0.085722774 
		-0.19548622 -0.0094637983 0.65938044 0.086380377 -0.0094637983 0.6800549 -0.086380377 
		-0.0094637983 -0.16329585 -0.086380377 -0.0094637983 -0.14262137 0.086380377 -0.0039116256 
		0.0020106372 0.11510552 -0.0039116256 0.0020106372 0.18313177 -0.0039116256 0.37446028 
		0.18313177 -0.0039116256 0.37446028 0.11510552 0.0039093718 0.36856952 -0.12834197 
		0.0039093718 0.14869851 -0.12834197 -0.10413831 0.18245286 -0.12834197 -0.10413831 
		0.29841781 -0.12834197 0.0081629716 0.29147696 -0.26092884 0.0081629716 0.22528212 
		-0.26092884 -0.081421681 0.23184046 -0.15416396 -0.081421681 0.2778466 -0.15416396;
	setAttr -s 36 ".vt[0:35]"  0.5 -0.49987897 0.5 0.5 0.500121 0.5 0.5 0.500121 -0.5
		 0.5 -0.49987897 -0.5 0.36216724 -0.3024897 0.66627121 0.36216724 0.42184472 0.66627121
		 0.20546307 -0.3024897 0.95085937 0.20546307 0.42184472 0.95085937 0.71567011 -0.17895055 -0.42081136
		 0.71567011 -0.17895055 0.42081136 0.71567011 0.382227 -0.42081136 0.71567011 0.382227 0.42081136
		 3.046031952 -0.098682582 -0.54654402 3.046031952 -0.098682582 0.28490293 3.046031952 0.3019591 -0.54654402
		 3.046031952 0.3019591 0.28490293 6.25107908 -0.055766106 -1.66328859 5.91802931 -0.055766106 -1.13154292
		 6.25107908 0.25904262 -1.66328859 5.91802931 0.25904262 -1.13154292 0.020141006 -0.79615098 0.5
		 0.020141006 -0.79615098 -0.5 0.020141006 0.64267576 -0.5 0.020141006 0.64267576 0.5
		 0.014588833 0.42184472 0.66627121 0.014588833 0.42184472 1.060031056 0.014588833 -0.3024897 1.060031056
		 0.014588833 -0.3024897 0.66627121 0.0067678355 -0.36735842 -0.74288845 0.0067678355 0.21388316 -0.74288845
		 0.43885696 0.1562956 -0.74288845 0.43885696 -0.24767378 -0.74288845 0.0025142357 -0.14110498 -1.20135152
		 0.0025142357 -0.012370273 -1.20135152 0.28296807 -0.025124848 -1.20135152 0.28296807 -0.11459687 -1.20135152;
	setAttr -s 64 ".ed[0:63]"  0 1 0 1 2 0 2 3 0 3 0 0 0 4 0 1 5 0 4 5 1
		 4 6 0 5 7 0 6 7 0 3 8 0 0 9 0 8 9 0 2 10 0 10 8 1 1 11 0 11 10 1 9 11 1 8 12 0 9 13 0
		 12 13 1 10 14 0 14 12 0 11 15 0 15 14 1 13 15 0 12 16 0 13 17 0 16 17 0 14 18 0 18 16 0
		 15 19 0 19 18 0 17 19 0 20 0 0 21 3 0 22 2 0 23 1 0 24 5 1 25 7 0 26 6 0 27 4 1 20 21 0
		 22 23 0 23 24 0 24 25 0 25 26 0 26 27 0 27 20 0 21 28 0 22 29 0 2 30 0 29 30 0 3 31 0
		 30 31 1 28 31 1 28 32 0 29 33 0 32 33 0 30 34 0 33 34 0 31 35 0 34 35 0 32 35 0;
	setAttr -s 29 -ch 116 ".fc[0:28]" -type "polyFaces" 
		f 4 46 40 9 -40
		mu 0 4 29 30 9 10
		f 4 43 37 1 -37
		mu 0 4 26 27 1 2
		f 4 58 60 62 -64
		mu 0 4 36 37 38 39
		f 4 42 35 3 -35
		mu 0 4 24 25 3 4
		f 4 -29 -31 -33 -34
		mu 0 4 19 20 21 22
		f 4 48 34 4 -42
		mu 0 4 31 23 0 7
		f 4 0 5 -7 -5
		mu 0 4 0 1 8 7
		f 4 -38 44 38 -6
		mu 0 4 1 27 28 8
		f 4 47 41 7 -41
		mu 0 4 30 31 7 9
		f 4 6 8 -10 -8
		mu 0 4 7 8 10 9
		f 4 -39 45 39 -9
		mu 0 4 8 28 29 10
		f 4 -4 10 12 -12
		mu 0 4 0 5 12 11
		f 4 -3 13 14 -11
		mu 0 4 5 6 13 12
		f 4 -2 15 16 -14
		mu 0 4 6 1 14 13
		f 4 -1 11 17 -16
		mu 0 4 1 0 11 14
		f 4 -13 18 20 -20
		mu 0 4 11 12 16 15
		f 4 -15 21 22 -19
		mu 0 4 12 13 17 16
		f 4 -17 23 24 -22
		mu 0 4 13 14 18 17
		f 4 -18 19 25 -24
		mu 0 4 14 11 15 18
		f 4 -21 26 28 -28
		mu 0 4 15 16 20 19
		f 4 -23 29 30 -27
		mu 0 4 16 17 21 20
		f 4 -25 31 32 -30
		mu 0 4 17 18 22 21
		f 4 -26 27 33 -32
		mu 0 4 18 15 19 22
		f 4 36 51 -53 -51
		mu 0 4 26 2 34 33
		f 4 2 53 -55 -52
		mu 0 4 2 3 35 34
		f 4 -36 49 55 -54
		mu 0 4 3 25 32 35
		f 4 52 59 -61 -58
		mu 0 4 33 34 38 37
		f 4 54 61 -63 -60
		mu 0 4 34 35 39 38
		f 4 -56 56 63 -62
		mu 0 4 35 32 36 39;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "transform1" -p "pCube1";
	rename -uid "34063313-413E-96A4-5717-69BF34714E52";
	setAttr ".v" no;
createNode mesh -n "pCubeShape1" -p "transform1";
	rename -uid "53714492-4FC3-D78E-096F-3DAC913BCA18";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.50503528118133545 0.5 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "pCube2";
	rename -uid "DDC0716B-41C1-2A57-543E-09B93EA4B4AE";
	setAttr ".t" -type "double3" -4.7624825917285509 0 0 ;
	setAttr ".s" -type "double3" -1 1 1 ;
	setAttr ".rp" -type "double3" 2.3892050299488203 0.18164191612701089 -0.35373838260992102 ;
	setAttr ".sp" -type "double3" 2.3892050299488203 0.18164191612701089 -0.35373838260992102 ;
createNode mesh -n "polySurfaceShape1" -p "pCube2";
	rename -uid "CA035531-49CF-820D-FA3C-6096E9F684F4";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.50503528118133545 0.5 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 40 ".uvst[0].uvsp[0:39]" -type "float2" 0.625 0 0.625 0.25
		 0.625 0.5 0.625 0.75 0.625 1 0.875 0 0.875 0.25 0.625 0 0.625 0.25 0.625 0 0.625
		 0.25 0.625 0 0.875 0 0.875 0.25 0.625 0.25 0.625 0 0.875 0 0.875 0.25 0.625 0.25
		 0.625 0 0.875 0 0.875 0.25 0.625 0.25 0.50503528 0 0.50503528 1 0.50503528 0.75 0.50503528
		 0.5 0.50503528 0.25 0.50503528 0.25 0.50503528 0.25 0.50503528 0 0.50503528 0 0.50503528
		 0.75 0.50503528 0.5 0.625 0.5 0.625 0.75 0.50503528 0.75 0.50503528 0.5 0.625 0.5
		 0.625 0.75;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 36 ".pt[0:35]" -type "float3"  -0.11864721 0.49423844 0.086380377 
		-0.11864721 -0.063161373 0.086380377 -0.11864721 -0.079739109 -0.086380377 -0.11864721 
		0.50639868 -0.086380377 -0.085940264 0.37446028 0.11510552 -0.085940264 0.0020106372 
		0.11510552 -0.04875524 0.37446028 0.16427119 -0.04875524 0.0020106372 0.16427119 
		-0.16982453 0.31093699 -0.072699688 -0.16982453 0.31093699 0.072699688 -0.16982453 
		0.022381896 -0.072699688 -0.16982453 0.022381896 0.072699688 -0.72280639 0.26966354 
		-0.094421357 -0.72280639 0.26966354 0.049220044 -0.72280639 0.063655309 -0.094421357 
		-0.72280639 0.063655309 0.049220044 -1.4833462 0.24759607 -0.28735098 -1.4043154 
		0.24759607 -0.19548622 -1.4833462 0.085722774 -0.28735098 -1.4043154 0.085722774 
		-0.19548622 -0.0094637983 0.65938044 0.086380377 -0.0094637983 0.6800549 -0.086380377 
		-0.0094637983 -0.16329585 -0.086380377 -0.0094637983 -0.14262137 0.086380377 -0.0039116256 
		0.0020106372 0.11510552 -0.0039116144 0.0020106372 0.18313177 -0.0039116256 0.37446028 
		0.18313177 -0.0039116256 0.37446028 0.11510552 0.0039093718 0.36856952 -0.12834197 
		0.0039093718 0.14869851 -0.12834197 -0.10413831 0.18245286 -0.12834197 -0.10413831 
		0.29841781 -0.12834197 0.0081629716 0.29147696 -0.26092884 0.0081629716 0.22528212 
		-0.26092884 -0.081421681 0.23184046 -0.15416396 -0.081421681 0.2778466 -0.15416396;
	setAttr -s 36 ".vt[0:35]"  0.5 -0.49987897 0.5 0.5 0.500121 0.5 0.5 0.500121 -0.5
		 0.5 -0.49987897 -0.5 0.36216724 -0.3024897 0.66627121 0.36216724 0.42184472 0.66627121
		 0.20546307 -0.3024897 0.95085937 0.20546307 0.42184472 0.95085937 0.71567011 -0.17895055 -0.42081136
		 0.71567011 -0.17895055 0.42081136 0.71567011 0.382227 -0.42081136 0.71567011 0.382227 0.42081136
		 3.046031952 -0.098682582 -0.54654402 3.046031952 -0.098682582 0.28490293 3.046031952 0.3019591 -0.54654402
		 3.046031952 0.3019591 0.28490293 6.25107908 -0.055766106 -1.66328859 5.91802931 -0.055766106 -1.13154292
		 6.25107908 0.25904262 -1.66328859 5.91802931 0.25904262 -1.13154292 0.020141006 -0.79615098 0.5
		 0.020141006 -0.79615098 -0.5 0.020141006 0.64267576 -0.5 0.020141006 0.64267576 0.5
		 0.014588833 0.42184472 0.66627121 0.014588833 0.42184472 1.060031056 0.014588833 -0.3024897 1.060031056
		 0.014588833 -0.3024897 0.66627121 0.0067678355 -0.36735842 -0.74288845 0.0067678355 0.21388316 -0.74288845
		 0.43885696 0.1562956 -0.74288845 0.43885696 -0.24767378 -0.74288845 0.0025142357 -0.14110498 -1.20135152
		 0.0025142357 -0.012370273 -1.20135152 0.28296807 -0.025124848 -1.20135152 0.28296807 -0.11459687 -1.20135152;
	setAttr -s 64 ".ed[0:63]"  0 1 0 1 2 0 2 3 0 3 0 0 0 4 0 1 5 0 4 5 1
		 4 6 0 5 7 0 6 7 0 3 8 0 0 9 0 8 9 0 2 10 0 10 8 1 1 11 0 11 10 1 9 11 1 8 12 0 9 13 0
		 12 13 1 10 14 0 14 12 0 11 15 0 15 14 1 13 15 0 12 16 0 13 17 0 16 17 0 14 18 0 18 16 0
		 15 19 0 19 18 0 17 19 0 20 0 0 21 3 0 22 2 0 23 1 0 24 5 1 25 7 0 26 6 0 27 4 1 20 21 0
		 22 23 0 23 24 0 24 25 0 25 26 0 26 27 0 27 20 0 21 28 0 22 29 0 2 30 0 29 30 0 3 31 0
		 30 31 1 28 31 1 28 32 0 29 33 0 32 33 0 30 34 0 33 34 0 31 35 0 34 35 0 32 35 0;
	setAttr -s 29 -ch 116 ".fc[0:28]" -type "polyFaces" 
		f 4 46 40 9 -40
		mu 0 4 29 30 9 10
		f 4 43 37 1 -37
		mu 0 4 26 27 1 2
		f 4 58 60 62 -64
		mu 0 4 36 37 38 39
		f 4 42 35 3 -35
		mu 0 4 24 25 3 4
		f 4 -29 -31 -33 -34
		mu 0 4 19 20 21 22
		f 4 48 34 4 -42
		mu 0 4 31 23 0 7
		f 4 0 5 -7 -5
		mu 0 4 0 1 8 7
		f 4 -38 44 38 -6
		mu 0 4 1 27 28 8
		f 4 47 41 7 -41
		mu 0 4 30 31 7 9
		f 4 6 8 -10 -8
		mu 0 4 7 8 10 9
		f 4 -39 45 39 -9
		mu 0 4 8 28 29 10
		f 4 -4 10 12 -12
		mu 0 4 0 5 12 11
		f 4 -3 13 14 -11
		mu 0 4 5 6 13 12
		f 4 -2 15 16 -14
		mu 0 4 6 1 14 13
		f 4 -1 11 17 -16
		mu 0 4 1 0 11 14
		f 4 -13 18 20 -20
		mu 0 4 11 12 16 15
		f 4 -15 21 22 -19
		mu 0 4 12 13 17 16
		f 4 -17 23 24 -22
		mu 0 4 13 14 18 17
		f 4 -18 19 25 -24
		mu 0 4 14 11 15 18
		f 4 -21 26 28 -28
		mu 0 4 15 16 20 19
		f 4 -23 29 30 -27
		mu 0 4 16 17 21 20
		f 4 -25 31 32 -30
		mu 0 4 17 18 22 21
		f 4 -26 27 33 -32
		mu 0 4 18 15 19 22
		f 4 36 51 -53 -51
		mu 0 4 26 2 34 33
		f 4 2 53 -55 -52
		mu 0 4 2 3 35 34
		f 4 -36 49 55 -54
		mu 0 4 3 25 32 35
		f 4 52 59 -61 -58
		mu 0 4 33 34 38 37
		f 4 54 61 -63 -60
		mu 0 4 34 35 39 38
		f 4 -56 56 63 -62
		mu 0 4 35 32 36 39;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "transform2" -p "pCube2";
	rename -uid "301C9F79-4588-585F-AE1C-33954BC04E2F";
	setAttr ".v" no;
createNode mesh -n "pCubeShape2" -p "transform2";
	rename -uid "4EBEF424-486D-1FEF-EDE2-2BA2199C7F1E";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.50503528118133545 0.5 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "pCube3";
	rename -uid "79AA9981-4A89-67C6-4BE0-39988406B643";
	setAttr ".t" -type "double3" -0.0071249155778596029 0 0.035844891102506216 ;
	setAttr -l on ".tx";
	setAttr -l on ".ty";
	setAttr -l on ".tz";
	setAttr -l on ".rx";
	setAttr -l on ".ry";
	setAttr -l on ".rz";
	setAttr -l on ".sx";
	setAttr -l on ".sy";
	setAttr -l on ".sz";
	setAttr ".rp" -type "double3" 0.0079637340845448712 0.18164190649986267 -0.35373836755752563 ;
	setAttr ".sp" -type "double3" 0.0079637340845448712 0.18164190649986267 -0.35373836755752563 ;
createNode mesh -n "pCube3Shape" -p "pCube3";
	rename -uid "9496B86D-4DE0-73E5-4C59-15B8DED5BA5F";
	setAttr -k off ".v";
	setAttr -s 4 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.4936520610935986 0.50956380367279053 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 1;
	setAttr ".vcs" 2;
createNode mesh -n "pCube3ShapeOrig" -p "pCube3";
	rename -uid "DB4D4EB7-49E1-2793-E1B8-4F92E4FBDB07";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode joint -n "joint1";
	rename -uid "69AF7316-4A0C-1C09-16EC-32962BB308B3";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" 1;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" -176.52625977634617 -6.509829694037836 151.83448589962717 ;
	setAttr ".bps" -type "matrix" -0.8759036097053261 0.46897684111140209 0.11337366981072093 0
		 0.46509696732754829 0.88321046331051278 -0.060200402669307637 0 -0.12836540611822625 4.1286418728248009e-16 -0.99172693949095825 0
		 -0.0071249157190322876 0 0.035844892263412476 1;
	setAttr ".radi" 0.52689875329201874;
createNode joint -n "joint2" -p "joint1";
	rename -uid "97E8829C-420A-2AEA-7E84-009AA9476A96";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" 1;
	setAttr ".oc" 1;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 8.4369000320204154e-16 -19.532226816827041 -27.967901555422532 ;
	setAttr ".bps" -type "matrix" -0.97757391793227921 3.0456923093063339e-16 -0.21059258054008825 0
		 2.7577432828831645e-16 1 3.6787618868534878e-16 0 0.21059258054008828 3.335941832773284e-16 -0.9775739179322791 0
		 -0.53704297542572033 0.28372904841976099 0.10443549340060354 1;
	setAttr ".radi" 0.50267981501710679;
createNode joint -n "joint3" -p "joint2";
	rename -uid "3D4C0AB3-49A0-6D3D-FFB4-B98A82703B13";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" 1;
	setAttr ".oc" 2;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 4.454261416682203e-15 -45.22872405879 -1.0693120030648934e-14 ;
	setAttr ".bps" -type "matrix" -0.53897948138319696 4.5132801245303442e-16 -0.84231889367857593 0
		 2.7577432828831645e-16 1 3.6787618868534878e-16 0 0.84231889367857593 1.8722198624596789e-17 -0.53897948138319685 0
		 -1.4343938827514648 0.24508678907431236 -0.11624468069034297 1;
	setAttr ".radi" 0.51772894237797795;
createNode joint -n "joint4" -p "joint3";
	rename -uid "7B3DA9B9-4CB1-4E42-F4FD-2DA2DDF540D1";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" 1;
	setAttr ".oc" 3;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 0 237.38580528814973 0 ;
	setAttr ".bps" -type "matrix" 1 -2.2748647645294474e-16 3.3306690738754696e-16 0
		 2.7577432828831645e-16 1 3.6787618868534878e-16 0 -2.2204460492503131e-16 -3.9025299304062885e-16 1 0
		 -2.0498726367950448 0.2431177724431933 -0.83077935395737823 1;
	setAttr ".radi" 0.51772894237797795;
createNode parentConstraint -n "joint4_parentConstraint1" -p "joint4";
	rename -uid "1F021748-4144-3528-C4EC-BC803ECB23B2";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle3W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" -1.1621721640597826e-08 8.8817841970012523e-16 
		3.2455128806674338e-08 ;
	setAttr ".tg[0].tor" -type "double3" 0 3.1805546814635168e-15 -89.999999999999986 ;
	setAttr ".lr" -type "double3" -3.6159824686052369e-14 2.1334656006695643e-14 -13.525841906536446 ;
	setAttr ".rst" -type "double3" 0.93359647513805677 -0.0019690166311194246 -0.13330985556087782 ;
	setAttr -k on ".w0";
createNode parentConstraint -n "joint3_parentConstraint1" -p "joint3";
	rename -uid "52488824-45AB-657C-565B-C5AEF0D884C5";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle2W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" -8.4681095469107959e-11 0 7.2863240441201071e-10 ;
	setAttr ".tg[0].tor" -type "double3" 180 57.385805288149726 90.000000000000014 ;
	setAttr ".lr" -type "double3" 1.6113037034807689 -0.014497876642836423 1.0309539890169155 ;
	setAttr ".rst" -type "double3" 0.9237004495703619 -0.038642259345448959 0.026755739172345888 ;
	setAttr ".rsrr" -type "double3" -5.5800010310046938e-15 1.2722218725854067e-14 3.0540630021504999e-15 ;
	setAttr -k on ".w0";
createNode parentConstraint -n "joint2_parentConstraint1" -p "joint2";
	rename -uid "246EC48D-489D-AD98-3C9C-228716D874B8";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle1W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" 2.3709472163346845e-09 1.1102230246251565e-16 
		-3.397319064957216e-09 ;
	setAttr ".tg[0].tor" -type "double3" 179.99999999999997 12.157081229359731 90 ;
	setAttr ".lr" -type "double3" -3.2138851245667657 -0.42564390164444726 -15.084745636597704 ;
	setAttr ".rst" -type "double3" 0.60499586237002101 -5.5511151231257827e-17 4.8572257327350599e-17 ;
	setAttr ".rsrr" -type "double3" -1.1724438604195735e-32 -3.975693351829396e-16 3.3793393490549868e-15 ;
	setAttr -k on ".w0";
createNode joint -n "joint5" -p "joint1";
	rename -uid "C281397E-41EB-C037-C18D-518E2B8CA3DA";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" 1;
	setAttr ".oc" 1;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 179.99999999999994 -2.741264890888695 152.03209844457746 ;
	setAttr ".bps" -type "matrix" 0.98445291160305493 1.9745579502588574e-17 -0.17564869722337095 0
		 -7.4310404647250586e-17 1 -5.866432439189324e-16 0 0.17564869722337095 6.0068676753308581e-16 0.98445291160305504 0
		 0.53872066736221336 0.2774495429994473 0.12194589181433289 1;
	setAttr ".radi" 0.5;
createNode joint -n "joint6" -p "joint5";
	rename -uid "82DBB54B-4B57-ACAF-D6B9-6782C608113F";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" 1;
	setAttr ".oc" 2;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 3.8345708613047406e-15 27.919757421322473 1.5425632422428878e-14 ;
	setAttr ".bps" -type "matrix" 0.78762196098182047 -2.6381501318700962e-16 -0.61615878357705123 0
		 -7.4310404647250586e-17 1 -5.866432439189324e-16 0 0.61615878357705123 5.4001492517927552e-16 0.78762196098182047 0
		 1.4360713958740232 0.26064657212376496 -0.072739540154728402 1;
	setAttr ".radi" 0.5;
createNode joint -n "joint7" -p "joint6";
	rename -uid "788C6028-4EE1-373E-E1FD-5EA24BC4939A";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" 1;
	setAttr ".oc" 3;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 9.814732647570942 158.9334640820453 25.700299973445269 ;
	setAttr ".bps" -type "matrix" -0.88375148842456885 -0.40467928995717817 0.23498506119988477 0
		 -0.3910903070382567 0.91445867718544505 0.10398893912800514 0 -0.25696629827290879 -3.1257009402090061e-17 -0.96642036482677596 0
		 2.0835468662855527 0.26373036969387265 -0.87263364489004469 1;
	setAttr ".radi" 0.5;
createNode parentConstraint -n "joint7_parentConstraint1" -p "joint7";
	rename -uid "28DA0798-4F99-8E02-F30D-8581FE520C25";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle6W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" -1.1975865632507521e-08 1.7509379723890106e-08 
		-1.4658366520592381e-08 ;
	setAttr ".tg[0].tor" -type "double3" 173.85847905980171 -13.590742184766579 114.60355170837394 ;
	setAttr ".lr" -type "double3" 2.564476658637969 1.4451504996334856 -11.05717433791455 ;
	setAttr ".rst" -type "double3" 1.0028276782573231 0.0030837975701080156 -0.23106646510468054 ;
	setAttr ".rsrr" -type "double3" 2.3854160110976376e-15 3.975693351829396e-15 3.1805546814635168e-15 ;
	setAttr -k on ".w0";
createNode parentConstraint -n "joint6_parentConstraint1" -p "joint6";
	rename -uid "69A9CE59-45C9-6082-81EB-539641AD59E1";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle5W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" -1.4188961894312513e-08 4.4408920985006262e-16 
		2.0493689939504733e-09 ;
	setAttr ".tg[0].tor" -type "double3" 0 38.036167899678482 -89.999999999999986 ;
	setAttr ".lr" -type "double3" -2.5590903399296341 0.073101389993788832 3.2719183174350723 ;
	setAttr ".rst" -type "double3" 0.91759577990630881 -0.016802970875682333 -0.034040153933098616 ;
	setAttr ".rsrr" -type "double3" 0 3.1805546814635168e-15 0 ;
	setAttr -k on ".w0";
createNode parentConstraint -n "joint5_parentConstraint1" -p "joint5";
	rename -uid "41661571-47B5-09B5-3303-9696F93490D6";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle4W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" -6.7710914097851571e-09 -2.2204460492503131e-16 
		-4.4412420963091392e-09 ;
	setAttr ".tg[0].tor" -type "double3" -2.4046806401635491e-14 10.116410478356007 
		-89.999999999999972 ;
	setAttr ".lr" -type "double3" 2.7121731781124776 0.36626525730654935 -15.379166571848343 ;
	setAttr ".rst" -type "double3" -0.33822912002542965 0.4937341498948612 -0.15545637072181068 ;
	setAttr ".rsrr" -type "double3" 2.2760844439223296e-14 2.1120870931593628e-15 1.9083328088781101e-14 ;
	setAttr -k on ".w0";
createNode parentConstraint -n "joint1_parentConstraint1" -p "joint1";
	rename -uid "9CDE3156-45FA-CBB7-858F-61B37658BB42";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle7W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" 0 -2.7665273913308117 0 ;
	setAttr ".tg[0].tor" -type "double3" -176.52625977634617 -6.509829694037836 151.83448589962717 ;
	setAttr ".rst" -type "double3" -0.0071249157190322876 0 0.035844892263412476 ;
	setAttr -k on ".w0";
createNode transform -n "nurbsCircle1";
	rename -uid "DF9AEBC3-4F94-2215-A1BA-5B8EC1398902";
	addAttr -ci true -k true -sn "blendParent1" -ln "blendParent1" -dv 1 -smn 0 -smx 
		1 -at "double";
	setAttr -k on ".blendParent1";
createNode nurbsCurve -n "nurbsCircleShape1" -p "nurbsCircle1";
	rename -uid "FA31645F-4951-3635-DE18-5090AEA12507";
	setAttr -k off ".v";
	setAttr ".tw" yes;
createNode parentConstraint -n "nurbsCircle1_parentConstraint1" -p "nurbsCircle1";
	rename -uid "7585170A-4E02-7B2B-6535-22BE5B00C065";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle7W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" -1.6671969578563863 -1.873875792363249 0.21579571378302181 ;
	setAttr ".tg[0].tor" -type "double3" 0 0 89.999999999999986 ;
	setAttr ".lr" -type "double3" 0 0 89.999999999999986 ;
	setAttr ".rst" -type "double3" -0.53704297542572021 0.28372904658317566 0.10443549603223799 ;
	setAttr ".rsrr" -type "double3" 0 0 89.999999999999986 ;
	setAttr -k on ".w0";
createNode transform -n "nurbsCircle2";
	rename -uid "D902306E-4AE9-1BDB-7CD7-8AAADF30F134";
	addAttr -ci true -k true -sn "blendParent1" -ln "blendParent1" -dv 1 -smn 0 -smx 
		1 -at "double";
	setAttr -k on ".blendParent1";
createNode nurbsCurve -n "nurbsCircleShape2" -p "nurbsCircle2";
	rename -uid "8D1F0E2D-4A42-DE53-D6F4-A6BC401D7D4A";
	setAttr -k off ".v";
	setAttr ".cc" -type "nurbsCurve" 
		3 8 2 no 3
		13 -2 -1 0 1 2 3 4 5 6 7 8 9 10
		11
		0.78361162489122449 4.7982373409884731e-17 -0.7836116248912246
		6.7857323231109122e-17 6.7857323231109122e-17 -1.1081941875543877
		-0.78361162489122449 4.7982373409884719e-17 -0.78361162489122438
		-1.1081941875543881 3.5177356190060272e-33 -5.7448982375248304e-17
		-0.78361162489122449 -4.7982373409884725e-17 0.78361162489122449
		-1.1100856969603225e-16 -6.7857323231109171e-17 1.1081941875543884
		0.78361162489122449 -4.7982373409884719e-17 0.78361162489122438
		1.1081941875543881 -9.2536792101100989e-33 1.511240500779959e-16
		0.78361162489122449 4.7982373409884731e-17 -0.7836116248912246
		6.7857323231109122e-17 6.7857323231109122e-17 -1.1081941875543877
		-0.78361162489122449 4.7982373409884719e-17 -0.78361162489122438
		;
createNode parentConstraint -n "nurbsCircle2_parentConstraint1" -p "nurbsCircle2";
	rename -uid "7D1A8387-4A98-64CE-A6D4-C39C5EE6EDFE";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle1W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" -0.049885377525665298 1.1584387594470913 -0.2848879614838975 ;
	setAttr ".lr" -type "double3" 0 0 105.43888299546742 ;
	setAttr ".rst" -type "double3" -1.4343938827514648 0.24508678913116458 -0.11624468117952347 ;
	setAttr ".rsrr" -type "double3" 0 0 89.999999999999986 ;
	setAttr -k on ".w0";
createNode transform -n "nurbsCircle3";
	rename -uid "510549B9-4D4B-7C9A-227D-AC89667846E7";
	addAttr -ci true -k true -sn "blendParent1" -ln "blendParent1" -dv 1 -smn 0 -smx 
		1 -at "double";
	setAttr -k on ".blendParent1";
createNode nurbsCurve -n "nurbsCircleShape3" -p "nurbsCircle3";
	rename -uid "7C7A0FA5-42F7-F124-66B5-F8A5A32A1A92";
	setAttr -k off ".v";
	setAttr ".cc" -type "nurbsCurve" 
		3 8 2 no 3
		13 -2 -1 0 1 2 3 4 5 6 7 8 9 10
		11
		0.78361162489122449 4.7982373409884731e-17 -0.7836116248912246
		6.7857323231109122e-17 6.7857323231109122e-17 -1.1081941875543877
		-0.78361162489122449 4.7982373409884719e-17 -0.78361162489122438
		-1.1081941875543881 3.5177356190060272e-33 -5.7448982375248304e-17
		-0.78361162489122449 -4.7982373409884725e-17 0.78361162489122449
		-1.1100856969603225e-16 -6.7857323231109171e-17 1.1081941875543884
		0.78361162489122449 -4.7982373409884719e-17 0.78361162489122438
		1.1081941875543881 -9.2536792101100989e-33 1.511240500779959e-16
		0.78361162489122449 4.7982373409884731e-17 -0.7836116248912246
		6.7857323231109122e-17 6.7857323231109122e-17 -1.1081941875543877
		-0.78361162489122449 4.7982373409884719e-17 -0.78361162489122438
		;
createNode parentConstraint -n "nurbsCircle3_parentConstraint1" -p "nurbsCircle3";
	rename -uid "F105D686-4389-268D-F07A-719CE751EB24";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle2W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" -0.0029328319937400593 0.91675314667987484 
		-1.0642965714524053 ;
	setAttr ".lr" -type "double3" 0 0 103.52584190653644 ;
	setAttr ".rst" -type "double3" -2.0498726367950439 0.24311777949333185 -0.83077937364578247 ;
	setAttr ".rsrr" -type "double3" 0 0 89.999999999999986 ;
	setAttr -k on ".w0";
createNode transform -n "nurbsCircle4";
	rename -uid "D1FCC019-4662-B832-46DC-CB92E0CC1CFE";
	addAttr -ci true -k true -sn "blendParent1" -ln "blendParent1" -dv 1 -smn 0 -smx 
		1 -at "double";
	setAttr -k on ".blendParent1";
createNode nurbsCurve -n "nurbsCircleShape4" -p "nurbsCircle4";
	rename -uid "4BFF2837-4030-3772-FFDC-348C36D1F3B8";
	setAttr -k off ".v";
	setAttr ".cc" -type "nurbsCurve" 
		3 8 2 no 3
		13 -2 -1 0 1 2 3 4 5 6 7 8 9 10
		11
		0.78361162489122449 4.7982373409884731e-17 -0.7836116248912246
		6.7857323231109122e-17 6.7857323231109122e-17 -1.1081941875543877
		-0.78361162489122449 4.7982373409884719e-17 -0.78361162489122438
		-1.1081941875543881 3.5177356190060272e-33 -5.7448982375248304e-17
		-0.78361162489122449 -4.7982373409884725e-17 0.78361162489122449
		-1.1100856969603225e-16 -6.7857323231109171e-17 1.1081941875543884
		0.78361162489122449 -4.7982373409884719e-17 0.78361162489122438
		1.1081941875543881 -9.2536792101100989e-33 1.511240500779959e-16
		0.78361162489122449 4.7982373409884731e-17 -0.7836116248912246
		6.7857323231109122e-17 6.7857323231109122e-17 -1.1081941875543877
		-0.78361162489122449 4.7982373409884719e-17 -0.78361162489122438
		;
createNode parentConstraint -n "nurbsCircle4_parentConstraint1" -p "nurbsCircle4";
	rename -uid "D710AE94-4065-DEB3-C7AE-918F2F6FA121";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle7W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" 1.7173071928820556 -1.8936319798253982 0.2708858994818284 ;
	setAttr ".tg[0].tor" -type "double3" 0 0 89.999999999999986 ;
	setAttr ".lr" -type "double3" 0 0 89.999999999999986 ;
	setAttr ".rst" -type "double3" 0.53872066736221313 0.27744954824447643 0.12194589525461197 ;
	setAttr ".rsrr" -type "double3" 0 0 89.999999999999986 ;
	setAttr -k on ".w0";
createNode transform -n "nurbsCircle5";
	rename -uid "67759262-416D-FC8E-C339-54B4BF273309";
	addAttr -ci true -k true -sn "blendParent1" -ln "blendParent1" -dv 1 -smn 0 -smx 
		1 -at "double";
	setAttr -k on ".blendParent1";
createNode nurbsCurve -n "nurbsCircleShape5" -p "nurbsCircle5";
	rename -uid "E454C5A7-4F26-FC5D-CA28-208253DC117F";
	setAttr -k off ".v";
	setAttr ".cc" -type "nurbsCurve" 
		3 8 2 no 3
		13 -2 -1 0 1 2 3 4 5 6 7 8 9 10
		11
		0.78361162489122449 4.7982373409884731e-17 -0.7836116248912246
		6.7857323231109122e-17 6.7857323231109122e-17 -1.1081941875543877
		-0.78361162489122449 4.7982373409884719e-17 -0.78361162489122438
		-1.1081941875543881 3.5177356190060272e-33 -5.7448982375248304e-17
		-0.78361162489122449 -4.7982373409884725e-17 0.78361162489122449
		-1.1100856969603225e-16 -6.7857323231109171e-17 1.1081941875543884
		0.78361162489122449 -4.7982373409884719e-17 0.78361162489122438
		1.1081941875543881 -9.2536792101100989e-33 1.511240500779959e-16
		0.78361162489122449 4.7982373409884731e-17 -0.7836116248912246
		6.7857323231109122e-17 6.7857323231109122e-17 -1.1081941875543877
		-0.78361162489122449 4.7982373409884719e-17 -0.78361162489122438
		;
createNode parentConstraint -n "nurbsCircle5_parentConstraint1" -p "nurbsCircle5";
	rename -uid "EE9D14B1-4750-4CB6-4627-0C9426AE4F6B";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle4W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" -0.021691857241221191 -1.1584385286065242 -0.25132994688110039 ;
	setAttr ".lr" -type "double3" 0 0 74.37212789771327 ;
	setAttr ".rst" -type "double3" 1.4360713958740234 0.26064658164978027 -0.072739541530609131 ;
	setAttr ".rsrr" -type "double3" 0 0 89.999999999999986 ;
	setAttr -k on ".w0";
createNode transform -n "nurbsCircle6";
	rename -uid "FE183A42-4748-6A32-943F-9A8076D162EC";
	addAttr -ci true -k true -sn "blendParent1" -ln "blendParent1" -dv 1 -smn 0 -smx 
		1 -at "double";
	setAttr -k on ".blendParent1";
createNode nurbsCurve -n "nurbsCircleShape6" -p "nurbsCircle6";
	rename -uid "26493A0A-433B-96A9-1CF6-B9BB7221DD7E";
	setAttr -k off ".v";
	setAttr ".cc" -type "nurbsCurve" 
		3 8 2 no 3
		13 -2 -1 0 1 2 3 4 5 6 7 8 9 10
		11
		0.78361162489122449 4.7982373409884731e-17 -0.7836116248912246
		6.7857323231109122e-17 6.7857323231109122e-17 -1.1081941875543877
		-0.78361162489122449 4.7982373409884719e-17 -0.78361162489122438
		-1.1081941875543881 3.5177356190060272e-33 -5.7448982375248304e-17
		-0.78361162489122449 -4.7982373409884725e-17 0.78361162489122449
		-1.1100856969603225e-16 -6.7857323231109171e-17 1.1081941875543884
		0.78361162489122449 -4.7982373409884719e-17 0.78361162489122438
		1.1081941875543881 -9.2536792101100989e-33 1.511240500779959e-16
		0.78361162489122449 4.7982373409884731e-17 -0.7836116248912246
		6.7857323231109122e-17 6.7857323231109122e-17 -1.1081941875543877
		-0.78361162489122449 4.7982373409884719e-17 -0.78361162489122438
		;
createNode parentConstraint -n "nurbsCircle6_parentConstraint1" -p "nurbsCircle6";
	rename -uid "9E0F1A7D-4367-FE8F-F6EE-EAB812F5088A";
	addAttr -dcb 0 -ci true -k true -sn "w0" -ln "nurbsCircle5W0" -dv 1 -min 0 -at "double";
	setAttr -k on ".nds";
	setAttr -k off ".v";
	setAttr -k off ".tx";
	setAttr -k off ".ty";
	setAttr -k off ".tz";
	setAttr -k off ".rx";
	setAttr -k off ".ry";
	setAttr -k off ".rz";
	setAttr -k off ".sx";
	setAttr -k off ".sy";
	setAttr -k off ".sz";
	setAttr ".erp" yes;
	setAttr ".tg[0].tot" -type "double3" 0.00459330079995518 -0.96441214377537765 -1.1914390599118856 ;
	setAttr ".lr" -type "double3" 0 0 78.527682902351401 ;
	setAttr ".rst" -type "double3" 2.0835468769073486 0.2637303769588471 -0.87263363599777211 ;
	setAttr ".rsrr" -type "double3" 0 0 89.999999999999986 ;
	setAttr -k on ".w0";
createNode transform -n "nurbsCircle7";
	rename -uid "C591913B-4F6D-0663-5DF6-DDBC930CDCF3";
	setAttr ".t" -type "double3" -0.0071249157190322876 0.87933991267857992 0.035844892263412476 ;
	setAttr ".s" -type "double3" 0.31784970408537389 0.31784970408537389 0.31784970408537389 ;
createNode nurbsCurve -n "nurbsCircleShape7" -p "nurbsCircle7";
	rename -uid "59C6DDCE-4604-8853-C15C-5B9E5FC2B206";
	setAttr -k off ".v";
	setAttr ".tw" yes;
createNode lightLinker -s -n "lightLinker1";
	rename -uid "1AAE82A5-46BF-8BD6-33B7-EE8962AEC40F";
	setAttr -s 3 ".lnk";
	setAttr -s 3 ".slnk";
createNode shapeEditorManager -n "shapeEditorManager";
	rename -uid "C33E1323-480A-CB39-5A4C-059F5C5E865A";
createNode poseInterpolatorManager -n "poseInterpolatorManager";
	rename -uid "B2EBD706-4694-2642-EF9D-28ACBB16195C";
createNode displayLayerManager -n "layerManager";
	rename -uid "03B5AE82-418A-7DD9-AE8F-BDA166669B6A";
createNode displayLayer -n "defaultLayer";
	rename -uid "4752FDB7-4A74-CC4E-43FF-5EA8EB00B16C";
createNode renderLayerManager -n "renderLayerManager";
	rename -uid "7D698562-4050-7EB3-0BB7-C8BB4398D9C4";
createNode renderLayer -n "defaultRenderLayer";
	rename -uid "858D7BD5-42D0-0964-3D4C-E0BB4F4A6536";
	setAttr ".g" yes;
createNode polyMergeVert -n "polyMergeVert1";
	rename -uid "6FC05D64-4E94-06AA-379E-D3AED2E03196";
	setAttr ".ics" -type "componentList" 2 "vtx[20:29]" "vtx[32:33]";
	setAttr ".ix" -type "matrix" -1 0 0 0 0 1 0 0 0 0 1 0 0.015927468169089742 0 0 1;
	setAttr ".d" 0.002;
	setAttr ".am" yes;
createNode polyMergeVert -n "polyMergeVert2";
	rename -uid "B83DA499-473E-59AF-EC1B-18BAA03B16AC";
	setAttr ".ics" -type "componentList" 2 "vtx[20:29]" "vtx[32:33]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".d" 0.002;
	setAttr ".am" yes;
createNode polyUnite -n "polyUnite1";
	rename -uid "AC7E76E5-4305-6079-DDB6-EA852526B434";
	setAttr -s 2 ".ip";
	setAttr -s 2 ".im";
createNode groupId -n "groupId1";
	rename -uid "6CCED9AB-4F19-4E27-C800-0EA45A85C5DA";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts1";
	rename -uid "2F4CFD52-4FD4-6DDF-6790-6FBC625D3868";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:28]";
createNode groupId -n "groupId2";
	rename -uid "56993B78-43B9-1D4A-87A5-37822863B331";
	setAttr ".ihi" 0;
createNode groupId -n "groupId3";
	rename -uid "F1554792-4316-18D8-0EB7-66A6496F68B3";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts2";
	rename -uid "60DEE5A5-4735-1E50-674F-489139A62438";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:28]";
createNode groupId -n "groupId4";
	rename -uid "C503C249-4C82-57BF-3EC1-8E966E70FFFA";
	setAttr ".ihi" 0;
createNode groupId -n "groupId5";
	rename -uid "92DE0004-4D36-C4AD-7A38-D4A88C4C5A0C";
	setAttr ".ihi" 0;
createNode polyMergeVert -n "polyMergeVert3";
	rename -uid "F3F2CDE9-4FE5-F5D3-F8D3-789233EF5B14";
	setAttr ".ics" -type "componentList" 4 "vtx[20:29]" "vtx[32:33]" "vtx[56:65]" "vtx[68:69]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".d" 0.202;
	setAttr ".am" yes;
createNode polyExtrudeFace -n "polyExtrudeFace1";
	rename -uid "4FC624F7-44EF-155D-FA16-C99898677A6C";
	setAttr ".ics" -type "componentList" 2 "f[0]" "f[29]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.0079637319 0.20968878 1.1791466 ;
	setAttr ".rs" 53006;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.14078035950660706 0.071970582008361816 1.1151305437088013 ;
	setAttr ".cbx" -type "double3" 0.15670782327651978 0.34740698337554932 1.2431628704071045 ;
createNode polyTweak -n "polyTweak1";
	rename -uid "A035A084-406C-2D74-D0A7-5688B45BA69E";
	setAttr ".uopa" yes;
	setAttr -s 12 ".tk";
	setAttr ".tk[5]" -type "float3" 0 -0.050884932 0 ;
	setAttr ".tk[6]" -type "float3" 0 0.025373982 0 ;
	setAttr ".tk[7]" -type "float3" 0 -0.1018224 0 ;
	setAttr ".tk[25]" -type "float3" 0 -0.076448373 0 ;
	setAttr ".tk[27]" -type "float3" 0 -0.075517647 0 ;
	setAttr ".tk[30]" -type "float3" 0.046246473 0 0 ;
	setAttr ".tk[31]" -type "float3" 0.046246473 0 0 ;
	setAttr ".tk[40]" -type "float3" 0 -0.050884932 0 ;
	setAttr ".tk[41]" -type "float3" 0 0.025373982 0 ;
	setAttr ".tk[42]" -type "float3" 0 -0.1018224 0 ;
	setAttr ".tk[55]" -type "float3" -0.034737598 0 0 ;
	setAttr ".tk[56]" -type "float3" -0.034737598 0 0 ;
createNode polyExtrudeFace -n "polyExtrudeFace2";
	rename -uid "CCC57737-46CF-9FB8-FD02-71AD0D309F05";
	setAttr ".ics" -type "componentList" 2 "f[0]" "f[29]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.0079637319 0.20968878 1.2328553 ;
	setAttr ".rs" 47624;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.080743104219436646 0.16678082942962646 1.1946778297424316 ;
	setAttr ".cbx" -type "double3" 0.096670567989349365 0.25259673595428467 1.2710328102111816 ;
createNode polyTweak -n "polyTweak2";
	rename -uid "82E015A6-48AA-EBC5-169F-60A352DF9BD3";
	setAttr ".uopa" yes;
	setAttr -s 6 ".tk[59:64]" -type "float3"  1.4967418e-09 -0.094810255
		 0.027869884 0.060037259 -0.077341847 0.079547308 0.060037259 0.077341869 0.079547308
		 -7.2843498e-10 0.094810247 0.027869884 -0.060037259 0.077341869 0.079547308 -0.060037259
		 -0.077341847 0.079547308;
createNode polyTweak -n "polyTweak3";
	rename -uid "D9FB514B-4823-BB66-3311-BBA400104B13";
	setAttr ".uopa" yes;
	setAttr -s 15 ".tk";
	setAttr ".tk[7]" -type "float3" 0 0 -0.042107858 ;
	setAttr ".tk[25]" -type "float3" 0 -0.010989583 -0.061731357 ;
	setAttr ".tk[42]" -type "float3" 0 0 -0.042107858 ;
	setAttr ".tk[59]" -type "float3" 0 0.014227608 0 ;
	setAttr ".tk[60]" -type "float3" 0 0.014227608 0 ;
	setAttr ".tk[61]" -type "float3" 0 -0.055149157 0.031957541 ;
	setAttr ".tk[62]" -type "float3" 0 -0.053536687 0.031957541 ;
	setAttr ".tk[63]" -type "float3" 0 -0.053536687 0.031957541 ;
	setAttr ".tk[64]" -type "float3" 0 0.014227608 0 ;
	setAttr ".tk[65]" -type "float3" 0 -0.059279338 0.18516006 ;
	setAttr ".tk[66]" -type "float3" 0.02534621 -0.059279338 0.18516006 ;
	setAttr ".tk[67]" -type "float3" 0.02534621 -0.01329452 0.18516006 ;
	setAttr ".tk[68]" -type "float3" 0 -0.0042482782 0.18516006 ;
	setAttr ".tk[69]" -type "float3" -0.023078393 -0.01329452 0.18516006 ;
	setAttr ".tk[70]" -type "float3" -0.023078393 -0.059279338 0.18516006 ;
createNode polySplit -n "polySplit1";
	rename -uid "25CEB85E-484D-6F2B-D11B-258CBC1EB095";
	setAttr -s 5 ".e[0:4]"  0.054065298 0.054065298 0.054065298 0.054065298
		 0.054065298;
	setAttr -s 5 ".d[0:4]"  -2147483568 -2147483565 -2147483563 -2147483567 -2147483568;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit2";
	rename -uid "D836BD5F-412B-CA4C-17BA-08890DF5C861";
	setAttr -s 5 ".e[0:4]"  0.91398901 0.91398901 0.91398901 0.91398901
		 0.91398901;
	setAttr -s 5 ".d[0:4]"  -2147483509 -2147483506 -2147483507 -2147483508 -2147483509;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit3";
	rename -uid "ABF90A00-44FF-078E-E28C-28846552EEA2";
	setAttr -s 5 ".e[0:4]"  0.060991298 0.060991298 0.060991298 0.060991298
		 0.060991298;
	setAttr -s 5 ".d[0:4]"  -2147483560 -2147483557 -2147483555 -2147483559 -2147483560;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit4";
	rename -uid "AE52D815-40CA-1D6E-0B85-438DB7987913";
	setAttr -s 5 ".e[0:4]"  0.092114799 0.092114799 0.092114799 0.092114799
		 0.092114799;
	setAttr -s 5 ".d[0:4]"  -2147483630 -2147483629 -2147483625 -2147483627 -2147483630;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit5";
	rename -uid "0AF0DAB7-437C-AFC5-2049-9D8A1ECA4937";
	setAttr -s 5 ".e[0:4]"  0.94049501 0.94049501 0.94049501 0.94049501
		 0.94049501;
	setAttr -s 5 ".d[0:4]"  -2147483485 -2147483482 -2147483483 -2147483484 -2147483485;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit6";
	rename -uid "C4AEF56C-4A65-F222-ADCF-4385C439D3DF";
	setAttr -s 5 ".e[0:4]"  0.059230801 0.059230801 0.059230801 0.059230801
		 0.059230801;
	setAttr -s 5 ".d[0:4]"  -2147483622 -2147483621 -2147483617 -2147483619 -2147483622;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode makeNurbCircle -n "makeNurbCircle1";
	rename -uid "BC419158-406D-B654-4349-2F8AEBC0E7A0";
	setAttr ".nr" -type "double3" 0 1 0 ;
createNode makeNurbCircle -n "makeNurbCircle2";
	rename -uid "8BC97E62-4010-E079-A914-5CA4DF2E2A4A";
	setAttr ".nr" -type "double3" 0 1 0 ;
createNode polyTweak -n "polyTweak4";
	rename -uid "F21FC112-43F9-DC6A-AF8A-84B634890FF9";
	setAttr ".uopa" yes;
	setAttr -s 47 ".tk";
	setAttr ".tk[4]" -type "float3" 0.023128377 0.0046618236 0 ;
	setAttr ".tk[12]" -type "float3" 0.8800289 0 0.057446387 ;
	setAttr ".tk[13]" -type "float3" 0.8800289 0 0.057446387 ;
	setAttr ".tk[14]" -type "float3" 0.8800289 0 0.057446387 ;
	setAttr ".tk[15]" -type "float3" 0.8800289 0 0.057446387 ;
	setAttr ".tk[16]" -type "float3" 2.5934422 0 0.69837773 ;
	setAttr ".tk[17]" -type "float3" 2.4550388 0 0.69837773 ;
	setAttr ".tk[18]" -type "float3" 2.5934422 0 0.69837773 ;
	setAttr ".tk[19]" -type "float3" 2.4550388 0 0.69837773 ;
	setAttr ".tk[20]" -type "float3" 0 0 2.9802322e-08 ;
	setAttr ".tk[25]" -type "float3" 0 0 -0.036927048 ;
	setAttr ".tk[26]" -type "float3" 0 0 -0.1200818 ;
	setAttr ".tk[27]" -type "float3" 0 0.00467186 0 ;
	setAttr ".tk[35]" -type "float3" 3.7252903e-09 0 1.8626451e-09 ;
	setAttr ".tk[39]" -type "float3" -0.032534961 0.00467186 0 ;
	setAttr ".tk[47]" -type "float3" -0.88002914 0 0.057446387 ;
	setAttr ".tk[48]" -type "float3" -0.88002914 0 0.057446387 ;
	setAttr ".tk[49]" -type "float3" -0.88002914 0 0.057446387 ;
	setAttr ".tk[50]" -type "float3" -0.88002914 0 0.057446387 ;
	setAttr ".tk[51]" -type "float3" -2.5934422 0 0.69837773 ;
	setAttr ".tk[52]" -type "float3" -2.4550388 0 0.69837773 ;
	setAttr ".tk[53]" -type "float3" -2.5934422 0 0.69837773 ;
	setAttr ".tk[54]" -type "float3" -2.4550388 0 0.69837773 ;
	setAttr ".tk[71]" -type "float3" -0.034518272 0 0 ;
	setAttr ".tk[72]" -type "float3" -0.034518272 0 0 ;
	setAttr ".tk[73]" -type "float3" -0.034518272 0 0 ;
	setAttr ".tk[74]" -type "float3" -0.034518272 0 0 ;
	setAttr ".tk[75]" -type "float3" -0.82506293 0 0.057446387 ;
	setAttr ".tk[76]" -type "float3" -0.82506293 0 0.057446387 ;
	setAttr ".tk[77]" -type "float3" -0.82506293 0 0.057446387 ;
	setAttr ".tk[78]" -type "float3" -0.82506293 0 0.057446387 ;
	setAttr ".tk[79]" -type "float3" -0.93669862 0 0.090878017 ;
	setAttr ".tk[80]" -type "float3" -0.93669862 0 0.090878017 ;
	setAttr ".tk[81]" -type "float3" -0.93081093 0 0.090878017 ;
	setAttr ".tk[82]" -type "float3" -0.93081093 0 0.090878017 ;
	setAttr ".tk[83]" -type "float3" 0.10271049 0 0 ;
	setAttr ".tk[84]" -type "float3" 0.084003419 0 0 ;
	setAttr ".tk[85]" -type "float3" 0.10271049 0 0 ;
	setAttr ".tk[86]" -type "float3" 0.10271049 0 0 ;
	setAttr ".tk[87]" -type "float3" 0.84353083 0 0.057446387 ;
	setAttr ".tk[88]" -type "float3" 0.84353083 0 0.057446387 ;
	setAttr ".tk[89]" -type "float3" 0.84353083 0 0.057446387 ;
	setAttr ".tk[90]" -type "float3" 0.84353083 0 0.057446387 ;
	setAttr ".tk[91]" -type "float3" 0.9350636 0 0.090878017 ;
	setAttr ".tk[92]" -type "float3" 0.9293443 0 0.090878017 ;
	setAttr ".tk[93]" -type "float3" 0.9293443 0 0.090878017 ;
	setAttr ".tk[94]" -type "float3" 0.9350636 0 0.090878017 ;
createNode polySplit -n "polySplit7";
	rename -uid "59712CBB-4982-C849-DAA9-918BA22D2729";
	setAttr -s 41 ".e[0:40]"  0.51016301 0.51016301 0.51016301 0.51016301
		 0.51016301 0.48983699 0.51016301 0.51016301 0.51016301 0.48983699 0.48983699 0.48983699
		 0.51016301 0.48983699 0.48983699 0.48983699 0.48983699 0.48983699 0.48983699 0.48983699
		 0.51016301 0.48983699 0.48983699 0.48983699 0.48983699 0.48983699 0.48983699 0.51016301
		 0.48983699 0.48983699 0.51016301 0.48983699 0.51016301 0.48983699 0.51016301 0.51016301
		 0.48983699 0.51016301 0.51016301 0.51016301 0.51016301;
	setAttr -s 41 ".d[0:40]"  -2147483588 -2147483595 -2147483646 -2147483634 -2147483478 -2147483473 
		-2147483626 -2147483462 -2147483618 -2147483615 -2147483464 -2147483623 -2147483471 -2147483480 -2147483631 -2147483648 -2147483642 -2147483639 
		-2147483530 -2147483518 -2147483515 -2147483511 -2147483524 -2147483577 -2147483580 -2147483586 -2147483569 -2147483503 -2147483496 -2147483561 
		-2147483487 -2147483553 -2147483556 -2147483489 -2147483564 -2147483494 -2147483505 -2147483572 -2147483584 -2147483541 -2147483536;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySoftEdge -n "polySoftEdge1";
	rename -uid "FC02E5D8-4F8D-B7FB-6E54-21AB26450EB3";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[*]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -0.0071249155778596029 0 0.035844891102506216 1;
	setAttr ".a" 180;
createNode polyTweak -n "polyTweak5";
	rename -uid "1965118B-4CC1-8A8B-A2F4-E58C831DF91D";
	setAttr ".uopa" yes;
	setAttr -s 95 ".tk";
	setAttr ".tk[0]" -type "float3" 0.040782135 0.0081138201 -0.040729683 ;
	setAttr ".tk[1]" -type "float3" 0.047827754 -0.0039467094 -0.018018642 ;
	setAttr ".tk[2]" -type "float3" 0.061910894 -0.011560739 0.0048592477 ;
	setAttr ".tk[3]" -type "float3" 0.051932901 0.007586983 0.010078976 ;
	setAttr ".tk[4]" -type "float3" 0.016885329 0 0 ;
	setAttr ".tk[5]" -type "float3" 0.034362085 -0.0019387671 -0.024137519 ;
	setAttr ".tk[6]" -type "float3" 0.010245757 0 0 ;
	setAttr ".tk[7]" -type "float3" 0.019052768 -0.00034038056 -0.033289194 ;
	setAttr ".tk[8]" -type "float3" 0.023303173 0.002151273 0.0060555073 ;
	setAttr ".tk[9]" -type "float3" 0.023303173 0.002151273 -0.036706202 ;
	setAttr ".tk[10]" -type "float3" 0.016878402 -0.0029315632 0.015867684 ;
	setAttr ".tk[11]" -type "float3" 0.016878402 -0.0029315632 -0.015104461 ;
	setAttr ".tk[12]" -type "float3" 0.062179968 0.00046187447 0.0099550178 ;
	setAttr ".tk[13]" -type "float3" 0.062179968 0.00046187447 -0.032289676 ;
	setAttr ".tk[14]" -type "float3" 0.045036722 -0.00170794 0.018692078 ;
	setAttr ".tk[15]" -type "float3" 0.045036722 -0.00170794 -0.011905596 ;
	setAttr ".tk[16]" -type "float3" 0.093853876 -0.00044138738 0.038927633 ;
	setAttr ".tk[17]" -type "float3" 0.088845 -0.00044138738 0.01191033 ;
	setAttr ".tk[18]" -type "float3" 0.067978002 -0.0010537106 0.03967683 ;
	setAttr ".tk[19]" -type "float3" 0.064350061 -0.0010537106 0.020108309 ;
	setAttr ".tk[30]" -type "float3" 0.058996361 -0.0089991307 0.013797677 ;
	setAttr ".tk[31]" -type "float3" 0.047908943 0.0056710187 0.022419812 ;
	setAttr ".tk[32]" -type "float3" 1.4862112e-11 0 0.048026476 ;
	setAttr ".tk[33]" -type "float3" 0.056268681 -0.0048560202 0.028994232 ;
	setAttr ".tk[34]" -type "float3" 0.044142969 0.00079682213 0.043400969 ;
	setAttr ".tk[35]" -type "float3" -0.040782131 0.0081138201 -0.040729683 ;
	setAttr ".tk[36]" -type "float3" -0.047827754 -0.0039467094 -0.018018642 ;
	setAttr ".tk[37]" -type "float3" -0.051273767 -0.020260694 0.018781871 ;
	setAttr ".tk[38]" -type "float3" -0.041791745 0.007586983 0.010078976 ;
	setAttr ".tk[39]" -type "float3" -0.016237387 0 0 ;
	setAttr ".tk[40]" -type "float3" -0.034362085 -0.0019387671 -0.024137519 ;
	setAttr ".tk[41]" -type "float3" -0.010245757 0 0 ;
	setAttr ".tk[42]" -type "float3" -0.019052774 -0.00034038056 -0.033289194 ;
	setAttr ".tk[43]" -type "float3" -0.023303175 0.002151273 0.0060555073 ;
	setAttr ".tk[44]" -type "float3" -0.023303175 0.002151273 -0.036706202 ;
	setAttr ".tk[45]" -type "float3" -0.016878402 -0.0029315632 0.015867684 ;
	setAttr ".tk[46]" -type "float3" -0.016878402 -0.0029315632 -0.015104461 ;
	setAttr ".tk[47]" -type "float3" -0.06217996 0.00046187447 0.0099550178 ;
	setAttr ".tk[48]" -type "float3" -0.06217996 0.00046187447 -0.032289676 ;
	setAttr ".tk[49]" -type "float3" -0.045036722 -0.00170794 0.018692078 ;
	setAttr ".tk[50]" -type "float3" -0.045036722 -0.00170794 -0.011905596 ;
	setAttr ".tk[51]" -type "float3" -0.093853861 -0.00044138738 0.038927633 ;
	setAttr ".tk[52]" -type "float3" -0.088844985 -0.00044138738 0.01191033 ;
	setAttr ".tk[53]" -type "float3" -0.067978002 -0.0010537106 0.03967683 ;
	setAttr ".tk[54]" -type "float3" -0.064350061 -0.0010537106 0.020108309 ;
	setAttr ".tk[55]" -type "float3" -0.048720349 -0.017699085 0.027720299 ;
	setAttr ".tk[56]" -type "float3" -0.038266398 0.0056710187 0.022419812 ;
	setAttr ".tk[57]" -type "float3" -0.045631543 -0.013555974 0.042916857 ;
	setAttr ".tk[58]" -type "float3" -0.034001827 0.00079682213 0.043400969 ;
	setAttr ".tk[60]" -type "float3" 0.0027835637 0.0016401064 -0.037106641 ;
	setAttr ".tk[64]" -type "float3" -0.0027835686 0.0016401064 -0.037106641 ;
	setAttr ".tk[66]" -type "float3" 0.0019882158 0.0039467094 -0.042916857 ;
	setAttr ".tk[70]" -type "float3" -0.0020593829 0.0039467094 -0.042916857 ;
	setAttr ".tk[71]" -type "float3" -0.02597091 0.0020599356 0.0064008934 ;
	setAttr ".tk[72]" -type "float3" -0.018810634 -0.0028654074 0.016117854 ;
	setAttr ".tk[73]" -type "float3" -0.018810634 -0.0028654074 -0.014834059 ;
	setAttr ".tk[74]" -type "float3" -0.02597091 0.0020599356 -0.036332905 ;
	setAttr ".tk[75]" -type "float3" -0.058296315 0.00059932529 0.0094352514 ;
	setAttr ".tk[76]" -type "float3" -0.058296315 0.00059932529 -0.03285151 ;
	setAttr ".tk[77]" -type "float3" -0.042223789 -0.0018074956 -0.012312527 ;
	setAttr ".tk[78]" -type "float3" -0.042223789 -0.0018074956 0.018315621 ;
	setAttr ".tk[79]" -type "float3" -0.066184208 0.00040678374 0.01196729 ;
	setAttr ".tk[80]" -type "float3" -0.047936946 -0.0016680375 0.020149564 ;
	setAttr ".tk[81]" -type "float3" -0.04763554 -0.0016680375 -0.0097754328 ;
	setAttr ".tk[82]" -type "float3" -0.065768026 0.00040678374 -0.029348694 ;
	setAttr ".tk[83]" -type "float3" 0.025946483 0.0019956531 0.0066439686 ;
	setAttr ".tk[84]" -type "float3" 0.026756935 0.0019956531 -0.036070131 ;
	setAttr ".tk[85]" -type "float3" 0.018792933 -0.0028188496 -0.014643745 ;
	setAttr ".tk[86]" -type "float3" 0.018792933 -0.0028188496 0.016293908 ;
	setAttr ".tk[87]" -type "float3" 0.05960127 0.0005531421 0.0096098892 ;
	setAttr ".tk[88]" -type "float3" 0.04316894 -0.0017740452 0.018442109 ;
	setAttr ".tk[89]" -type "float3" 0.04316894 -0.0017740452 -0.012175793 ;
	setAttr ".tk[90]" -type "float3" 0.05960127 0.0005531421 -0.032662716 ;
	setAttr ".tk[91]" -type "float3" 0.066068567 0.00040837354 0.011867393 ;
	setAttr ".tk[92]" -type "float3" 0.065664485 0.00040837354 -0.029475365 ;
	setAttr ".tk[93]" -type "float3" 0.04756052 -0.0016691894 -0.0098672006 ;
	setAttr ".tk[94]" -type "float3" 0.047853194 -0.0016691894 0.020077214 ;
	setAttr ".tk[95]" -type "float3" 0 0 0.1185497 ;
	setAttr ".tk[96]" -type "float3" -0.011089908 0 0 ;
	setAttr ".tk[97]" -type "float3" -0.0010448378 0 -0.027427152 ;
	setAttr ".tk[98]" -type "float3" 0.01004507 0 -0.027427152 ;
	setAttr ".tk[99]" -type "float3" 0.016012266 0 0.00014758157 ;
	setAttr ".tk[100]" -type "float3" 0.019904371 0 -0.034923028 ;
	setAttr ".tk[101]" -type "float3" 0.020325668 0 -0.037607495 ;
	setAttr ".tk[102]" -type "float3" 0.022414368 0 -0.041622546 ;
	setAttr ".tk[103]" -type "float3" 0.05132591 0 -0.069995299 ;
	setAttr ".tk[127]" -type "float3" -0.049600698 0 -0.033965588 ;
	setAttr ".tk[128]" -type "float3" -0.049600698 0 -0.033965588 ;
	setAttr ".tk[129]" -type "float3" -0.01282479 0 -0.041102812 ;
	setAttr ".tk[130]" -type "float3" -0.01282479 0 -0.041102812 ;
	setAttr ".tk[131]" -type "float3" 0 0 -0.045354318 ;
	setAttr ".tk[132]" -type "float3" 0 0 -0.045354318 ;
	setAttr ".tk[133]" -type "float3" 0.011821439 0 0 ;
	setAttr ".tk[134]" -type "float3" 0.011821439 0 0 ;
	setAttr ".tk[135]" -type "float3" 0 0 0.11511794 ;
createNode polyDelEdge -n "polyDelEdge1";
	rename -uid "FE79AFB8-4340-310C-032D-CB8B84A4EC2D";
	setAttr ".ics" -type "componentList" 2 "e[58]" "e[110]";
	setAttr ".cv" yes;
createNode polySplit -n "polySplit8";
	rename -uid "CFDDD170-4BBB-ADBE-2D28-A897D14AEBC7";
	setAttr -s 3 ".e[0:2]"  1 0.92348403 0;
	setAttr -s 3 ".d[0:2]"  -2147483540 -2147483592 -2147483465;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySoftEdge -n "polySoftEdge2";
	rename -uid "59157EF0-446A-0657-DC5D-52877102FB4E";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[*]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -0.0071249155778596029 0 0.035844891102506216 1;
	setAttr ".a" 180;
createNode skinCluster -n "skinCluster1";
	rename -uid "6958A7F0-4B41-9613-B88A-DBBFB7B75452";
	setAttr -s 135 ".wl";
	setAttr ".wl[0:99].w"
		5 0 0.56798418465031042 1 0.37241908810799929 2 0.014604887911417097 
		4 0.041533595607973699 5 0.0034582437222995472
		5 0 0.47296224768871065 1 0.46253150150003158 2 0.014588132225712164 
		4 0.046305332109039941 5 0.0036127864765055587
		5 0 0.48214501010765054 1 0.39665567522986483 2 0.043751183447572235 
		3 0.0096806270337547167 4 0.067767504181157665
		5 0 0.59093894027831206 1 0.30395902563459809 2 0.04011686235465791 
		3 0.0089111903718015542 4 0.056073981360630409
		5 0 0.47603545626688398 1 0.36213105614924296 2 0.027282761624492707 
		4 0.12281501140388616 5 0.011735714555494268
		5 0 0.43801223558973124 1 0.40556466657655932 2 0.025594848484789066 
		4 0.12020266404135449 5 0.01062558530756581
		5 0 0.37676602925797209 1 0.31979068512261111 2 0.048557756818119538 
		4 0.22166435305614776 5 0.033221175745149489
		5 0 0.36917049381433764 1 0.33496472751069478 2 0.042195955937606597 
		4 0.22459017415966007 5 0.029078648577700972
		5 0 0.47656833040426849 1 0.4469273095075767 2 0.047972895043438657 
		3 0.0071692671858286762 4 0.02136219785888754
		5 0 0.49635479244054509 1 0.4840416207656163 2 0.0096351030124384989 
		3 0.00085842145219945263 4 0.0091100623292005526
		5 0 0.44578805892375473 1 0.48032194665294775 2 0.046505243573760076 
		3 0.0068575165181193436 4 0.020527234331418127
		5 0 0.48952608681402549 1 0.48952608681402537 2 0.010264759078997486 
		3 0.00093208625919260536 4 0.0097509810337590794
		5 0 0.0067993274528918048 1 0.24516814811366733 2 0.71922704345001931 
		3 0.02829536822176134 4 0.00051011276166019071
		5 0 0.047833814477248569 1 0.53029815417077186 2 0.41190119871186676 
		3 0.0078047004204454418 4 0.0021621322196674134
		5 0 0.006405592021765368 1 0.23686464966088511 2 0.72820133023169398 
		3 0.028049760470637362 4 0.00047866761501829324
		5 0 0.054057854188742871 1 0.51767199963444988 2 0.41644462171680202 
		3 0.0092638554528416839 4 0.0025616690071634551
		5 0 0.00047211498024875997 1 0.0032160288563531135 2 0.49810359561227241 
		3 0.49810359561227208 4 0.00010466493885373968
		5 0 2.1957422578097409e-05 1 0.00056019005865064219 2 0.97354781749379549 
		3 0.02586700882740503 4 3.0261975707995972e-06
		5 0 0.00052178184839704155 1 0.0035055684682063007 2 0.4979281551494969 
		3 0.49792815514949712 4 0.000116339384402556
		5 0 8.2716261766904145e-05 1 0.0019911242217990149 2 0.91500444178209628 
		3 0.0829101924326867 4 1.1525301651004293e-05
		5 0 0.65059572670250798 1 0.15802513911551719 2 0.011256675350511095 
		4 0.16844312598658318 5 0.011679332844880496
		5 0 0.73058784794136911 1 0.12100146460311878 2 0.016488059680399864 
		4 0.11612151731234718 5 0.015801110462764997
		5 0 0.46762439495303121 1 0.24784007923850737 2 0.026739176917973981 
		4 0.23179897467792074 5 0.02599737421256661
		5 0 0.40761869061763917 1 0.27463010296880597 2 0.014061691418410772 
		4 0.28885991275220529 5 0.014829602242938833
		5 0 0.38310345457978767 1 0.27851245194159496 2 0.02117293318799528 
		4 0.29477465116320856 5 0.022436509127413615
		5 0 0.33730178654551457 1 0.28067954734390516 2 0.04196003586426042 
		4 0.29543740295683213 5 0.044621227289487836
		5 0 0.36921444236000495 1 0.26634964857186477 2 0.040338590778641659 
		4 0.28141574689792875 5 0.042681571391559918
		5 0 0.49032924995664284 1 0.22714825213741102 2 0.019542777676881729 
		4 0.24246799329401286 5 0.020511726935051617
		5 0 0.53209308084696705 1 0.19548350389356042 2 0.044109075960797411 
		4 0.18652831401765901 5 0.041786025281016131
		5 0 0.42950307846668012 1 0.24287565243966527 2 0.050277589616589048 
		4 0.22935436362111655 5 0.047989315855949094
		5 0 0.43382272715993642 1 0.32427886812630241 2 0.07635748935752755 
		4 0.13828469432151083 5 0.027256221034722798
		5 0 0.49016044139641923 1 0.28939168966406992 2 0.072763627233628184 
		3 0.025234708346793262 4 0.12244953335908947
		5 0 0.34483781632224836 1 0.22833949513856744 2 0.10661323113739622 
		4 0.21929669449667716 5 0.10091276290511085
		5 0 0.35592359686862746 1 0.25862705318729157 2 0.12299818857357861 
		3 0.076075365948711574 4 0.18637579542179084
		5 0 0.56357222570999099 1 0.03654204932213382 2 0.0030723977588736785 
		4 0.38266156064899803 5 0.014151766560003585
		5 0 0.47467206595285943 1 0.041066065347500763 2 0.0031835471367388096 
		4 0.4666933955796807 5 0.014384925983220305
		5 0 0.48753465857345435 1 0.069650487299729308 4 0.39062768868098108 
		5 0.04326153293143762 6 0.0089256325143975898
		5 0 0.58635740038683704 1 0.059549043688028831 4 0.30417209447281673 
		5 0.041129531256429312 6 0.0087919301958880487
		5 0 0.47789019491526458 1 0.11424205530026939 2 0.010917046053291083 
		4 0.36973248714643475 5 0.027218216584740038
		5 0 0.44192944761563918 1 0.10851120087083534 2 0.0095675653242981561 
		4 0.41408072519981076 5 0.025911060989416559
		5 0 0.38118860278302963 1 0.20589301685610623 2 0.03080182364176566 
		4 0.33162496831709459 5 0.050491588402003879
		5 0 0.3753919639660584 1 0.20800833668750249 2 0.02676909310834643 
		4 0.34594958140444465 5 0.0438810248336481
		5 0 0.48280758823410819 1 0.024303472854069644 4 0.43610690470735497 
		5 0.049629969548474061 6 0.0071520646559930781
		5 0 0.4949749756608865 1 0.0074754631017510528 2 0.00070019549983347282 
		4 0.48809137982260259 5 0.0087579859149265454
		5 0 0.44717052212492053 1 0.024000544876361506 4 0.47164288442290081 
		5 0.050128598270220801 6 0.007057450305596469
		5 0 0.49055471430888598 1 0.0083117501261536064 2 0.00078179517217289254 
		4 0.49055471430888598 5 0.0097970260839016481
		5 0 0.0087525348079072161 1 0.00068914282215385249 4 0.22025794444141039 
		5 0.74091483374416067 6 0.029385544184367847
		5 0 0.036740813965393006 1 0.0016043819213339876 4 0.53307489298611099 
		5 0.42356010856242998 6 0.0050198025647319611
		5 0 0.0074091058265134064 1 0.00058347666974188379 4 0.20356886524684242 
		5 0.7615533441652953 6 0.026885208091606983
		5 0 0.040465271780405328 1 0.001864351575229659 4 0.52006485516039191 
		5 0.43171208325461008 6 0.0058934382293630303
		5 0 0.00029363513958788248 1 6.7231961227490569e-05 4 0.0018105966833356396 
		5 0.4989142681079245 6 0.4989142681079245
		5 0 2.6541226204447869e-05 1 3.7516787869842243e-06 4 0.00058142080608809973 
		5 0.98397401160125542 6 0.015414274687665108
		5 0 0.00028795596632767305 1 6.6363303531994977e-05 4 0.0017673939357537153 
		5 0.49893914339719331 6 0.49893914339719331
		5 0 5.4927329264072236e-05 1 7.8591497573546398e-06 4 0.0011679007504938001 
		5 0.96588040407006504 6 0.032888908700419772
		5 0 0.4373374582767211 1 0.13974507957709215 2 0.02672010476320338 
		4 0.32073788248909102 5 0.075459474893892348
		5 0 0.4863612098814093 1 0.12581197899087027 2 0.025538103801249483 
		4 0.28855975449974203 5 0.073728952826728922
		5 0 0.35764543401256199 1 0.19449639285306489 4 0.25289549180551268 
		5 0.11976574338661644 6 0.075196937942244038
		5 0 0.32842022133300158 1 0.27724122900336101 2 0.050023167608432513 
		4 0.2911555419936695 5 0.053159840061535395
		5 0 0.34920384969296314 1 0.31006675555891361 2 0.047958449918691082 
		4 0.25368781511829086 5 0.03908312971114139
		5 0 0.35062425027371863 1 0.30112578735839479 2 0.053341153383876024 
		4 0.25130182979658788 5 0.043606979187422595
		5 0 0.33645355155838746 1 0.27105142501773738 2 0.05227842397476521 
		4 0.2848034511078224 5 0.05541314834128741
		5 0 0.35549280765052321 1 0.23538803351785309 2 0.040589483331879034 
		4 0.31274022529740525 5 0.055789450202339379
		5 0 0.35534690999711155 1 0.23687555925545059 2 0.036216814274948106 
		4 0.32139920941368472 5 0.050161507058805024
		5 0 0.31419385407517819 1 0.27244855513507932 2 0.062285974824714603 
		4 0.28496959350891859 5 0.066102022456109327
		5 0 0.32782128540539573 1 0.29289812436483509 2 0.059064506914485983 
		4 0.26716701269437781 5 0.053049070620905296
		5 0 0.32484733506112407 1 0.29090314295539493 2 0.062253341010623464 
		4 0.26612012956026265 5 0.055876051412594871
		5 0 0.31589813366417813 1 0.27155145635512268 2 0.062334340101158119 
		4 0.28409431647506433 5 0.066121753404476666
		5 0 0.33024204827714471 1 0.25025745860044768 2 0.051940775317745005 
		4 0.30203034101313775 5 0.065529376791524777
		5 0 0.33350369690028731 1 0.2508458406713252 2 0.049257028151413806 
		4 0.30422987126310075 5 0.062163563013873012
		5 0 0.43601949872581214 1 0.021083649677452954 4 0.47081316090430164 
		5 0.063479571977837726 6 0.0086041187145955017
		5 0 0.41930983142381234 1 0.020016361093229518 4 0.49028761130342008 
		5 0.062176489645835671 6 0.0082097065337024249
		5 0 0.49074256679627837 1 0.0065078768642206726 4 0.49074256679627815 
		5 0.011248080685565116 6 0.00075890885765772238
		5 0 0.49173376876661662 1 0.0058321781049085397 4 0.49173376876661662 
		5 0.010030679952021169 6 0.00066960440983712796
		5 0 0.018040169065272036 1 0.0012718350545008568 4 0.32564519380905432 
		5 0.62434706728602063 6 0.030695734785152119
		5 0 0.055502936431313613 1 0.0020168413000925963 4 0.58841648819677694 
		5 0.34948451017794291 6 0.0045792238938739864
		5 0 0.060022527084724817 1 0.0023147579962425648 4 0.56899371736823567 
		5 0.36334875674401546 6 0.0053202408067815326
		5 0 0.015773327885895031 1 0.0011076826321020185 4 0.31311705271984075 
		5 0.64137198637529713 6 0.028629950386864934
		5 0 0.0043988384963417945 1 0.0004050233298951094 4 0.11329416077468635 
		5 0.84639574460169531 6 0.035506232797381514
		5 0 0.0036061061248358083 1 0.00033293292420112527 4 0.098619981332762952 
		5 0.86541158181807498 6 0.032029397800125099
		5 0 0.020060028202174597 1 0.0010295767009282271 4 0.49541234270291001 
		5 0.47859644407643515 6 0.0049016083175520203
		5 0 0.017416447404368519 1 0.00085285176042889895 4 0.50195966415346061 
		5 0.47574675460813742 6 0.0040242820736044781
		5 0 0.42627696799313014 1 0.48338592215030407 2 0.062621015470267549 
		3 0.008868115146843129 4 0.018847979239455095
		5 0 0.49059575892822582 1 0.49059575892822604 2 0.0113696226412457 
		3 0.00088887311836447088 4 0.0065499863839378804
		5 0 0.49031060824012951 1 0.4903106082401294 2 0.011189129926399373 
		3 0.00091936589882956113 4 0.0072702876945121495
		5 0 0.41565016296708507 1 0.49951452857792605 2 0.059087852067106587 
		3 0.008241254956172615 4 0.017506201431709861
		5 0 0.011228833322608094 1 0.32106439902130895 2 0.63792176709628257 
		3 0.029004583255463078 4 0.00078041730433737826
		5 0 0.010585631652651552 1 0.31380125969162598 2 0.64629878475305935 
		3 0.028583863496710129 4 0.00073046040595293234
		5 0 0.068546009276142419 1 0.54592713168396712 2 0.37414668744936741 
		3 0.0084776946589003883 4 0.0029024769316227271
		5 0 0.061437110148128117 1 0.56240345766323341 2 0.36648395980891896 
		3 0.0072010310890905137 4 0.002474441290628938
		5 0 0.0035061539540199569 1 0.12699226496887198 2 0.83376730603336036 
		3 0.035427199407924805 4 0.00030707563582293258
		5 0 0.024822171141417285 1 0.50279095889677017 2 0.46440171080557857 
		3 0.0067426739634832734 4 0.001242485192750673
		5 0 0.029445214804164017 1 0.49531561639861627 2 0.46536955684614184 
		3 0.0083327628068567 4 0.0015368491442211564
		5 0 0.0033145471494576348 1 0.12062034182144221 2 0.84013965413245106 
		3 0.035635389153054156 4 0.00029006774359480544
		5 0 0.36192574376988629 1 0.27125134002315987 2 0.12296913181125799 
		3 0.070722674429026133 4 0.17313110996666967
		5 0 0.44092028883952789 1 0.32597117262314906 2 0.088487579827396315 
		3 0.03082251633446869 4 0.11379844237545816
		5 0 0.50289419606048458 1 0.3797924249507113 2 0.052448571650978 
		3 0.011389966094902395 4 0.053474841242923753
		5 0 0.44247776585387855 1 0.47034753716309191 2 0.055812461123768767 
		3 0.0086426610057098643 4 0.022719574853550879
		5 0 0.41000339646822936 1 0.50150253319890836 2 0.062289346927474828 
		3 0.0086818349680360361 4 0.017522888437351475
		5 0 0.0092109086067975639 1 0.25024885530496793 2 0.70078362311753417 
		3 0.039046533136617122 4 0.00071007983408319776
		5 0 0.0054406258986628726 1 0.17293578381884131 2 0.78216624419660508 
		3 0.039004008542359497 4 0.00045333754353126443;
	setAttr ".wl[100:134].w"
		5 0 0.0027142165980925548 1 0.079579780492114047 2 0.86631442986008333 
		3 0.051127959727446735 4 0.00026361332226334627
		5 0 0.0011430618159345553 1 0.006660996812895956 2 0.49596064816269386 
		3 0.49596064816269386 4 0.00027464504578175437
		5 0 6.6493810779912014e-05 1 0.0013925946196405778 2 0.84196197088396374 
		3 0.15656909496648991 4 9.8457191256868771e-06
		5 0 0.025518791932860767 1 0.4847613711387998 2 0.47913355315636036 
		3 0.0091251918999137333 4 0.0014610918720651639
		5 0 0.046832438370031551 1 0.49821192747931931 2 0.44244884732592576 
		3 0.010067379543112582 4 0.0024394072816107549
		5 0 0.05889370885422264 1 0.5222924932288352 2 0.40691975332225605 
		3 0.0091468285415117652 4 0.0027472160531744096
		5 0 0.48978621024783459 1 0.48978621024783459 2 0.012606903262427737 
		3 0.00097502836700808036 4 0.0068456478748950191
		5 0 0.48959886400118025 1 0.48959886400118025 2 0.010720042004368465 
		3 0.00093507526947122751 4 0.0091471547237997487
		5 0 0.49703909624156806 1 0.45097969838855867 2 0.014525556564918491 
		4 0.03457116101190371 5 0.0028844877930509823
		5 0 0.4538050250809143 1 0.39744700495822838 2 0.027154518535327639 
		4 0.11120067868782718 5 0.010392772737702461
		5 0 0.37234416028180939 1 0.33179457259052547 2 0.047111344105803331 
		4 0.21777294526184496 5 0.030976977760016823
		5 0 0.34763985057825075 1 0.30525555772104357 2 0.051964131633394098 
		4 0.25276583952069082 5 0.042374620546620737
		5 0 0.32413768327442399 1 0.29133514923006953 2 0.062225317137834239 
		4 0.26644434100106495 5 0.055857509356607354
		5 0 0.3150605533839958 1 0.2719968130123176 2 0.062305295081063215 
		4 0.28453073194026607 5 0.066106606582357147
		5 0 0.32961467287431095 1 0.25055331221295957 2 0.051907715004637714 
		4 0.30241851299593231 5 0.065505786912159303
		5 0 0.3531185253760401 1 0.23650374150462239 2 0.039361438562083635 
		4 0.31664575600875267 5 0.054370538548501275
		5 0 0.37763003689787816 1 0.20176864325267824 2 0.028583726358953069 
		4 0.34305081374513141 5 0.048966779745359068
		5 0 0.45634598907579699 1 0.10161943052391433 2 0.0094771532104660789 
		4 0.40535765186961498 5 0.027199775320207679
		5 0 0.49599425408583686 1 0.029819991312761071 2 0.0024928536248919569 
		4 0.45772782226382502 5 0.013965078712685163
		5 0 0.49088563410853214 1 0.0075654954638376759 2 0.0007291485171505124 
		4 0.49088563410853203 5 0.0099340878019477056
		5 0 0.4906913431987161 1 0.0060540814603006997 4 0.4906913431987161 
		5 0.011803438509055525 6 0.00075979363321153229
		5 0 0.051644595772447066 1 0.00219059729503524 4 0.54181661652795532 
		5 0.39863822679873856 6 0.0057099636058237533
		5 0 0.035357579921915225 1 0.0017866693640144877 4 0.50023302810535308 
		5 0.45620492012300801 6 0.0064178024857091424
		5 0 0.017688325883545238 1 0.00099361747905723336 4 0.48824173523287284 
		5 0.48765213381132422 6 0.0054241875932006176
		5 0 6.1543471791800997e-05 1 9.3381431778992371e-06 4 0.0011441084529786069 
		5 0.92214700825985763 6 0.076638001672194062
		5 0 0.00053835574783141367 1 0.00013066443306705634 4 0.0030081089623750076 
		5 0.49816143542836333 6 0.49816143542836333
		5 0 0.0041004917877308384 1 0.00040536647550092536 4 0.089132048388726667 
		5 0.85894783944030151 6 0.047414253907739914
		5 0 0.0061420141275872189 1 0.00054476638195606717 4 0.1433942712901207 
		5 0.8117573797683505 6 0.038161568431985579
		5 0 0.013775509136653149 1 0.0011002416842865262 4 0.24691392037870671 
		5 0.69729135070097681 6 0.040918978099376849
		5 0 0.39989570953485315 1 0.023216230097457453 4 0.48304562124301847 
		5 0.081886836495347004 6 0.011955602629323957
		5 0 0.43786934517526543 1 0.027444591656251908 4 0.45943365379364898 
		5 0.065205385508704805 6 0.010047023866128896
		5 0 0.51065744089467946 1 0.052642103182921743 4 0.37793254731017917 
		5 0.049008576378714135 6 0.009759332233505497
		5 0 0.44029032323857997 1 0.12001508484418662 4 0.32091006172400988 
		5 0.088220385182810696 6 0.030564145010412862
		5 0 0.36446814631015728 1 0.18421108542640188 4 0.2639070507782128 
		5 0.11823708245575112 6 0.069176635029476893
		5 0 0.35036878045636893 1 0.22971047656338245 2 0.10263444757012506 
		4 0.22038551413954921 5 0.09690078127057436;
	setAttr -s 7 ".pm";
	setAttr ".pm[0]" -type "matrix" -0.87590360970532632 0.46509696732754835 -0.12836540611822628 0
		 0.4689768411114022 0.88321046331051289 4.163336342344338e-16 0 0.11337366981072096 -0.060200402669307644 -0.99172693949095847 0
		 -0.010304606377019592 0.0054716536412816761 0.034633752600945471 1;
	setAttr ".pm[1]" -type "matrix" -0.9775739179322791 2.2748647645294459e-16 0.21059258054008825 0
		 3.4706178646445207e-16 1 3.0155013964618475e-16 0 -0.21059258054008828 3.9025299304062889e-16 -0.97757391793227921 0
		 -0.50300586552971971 -0.28372904841976093 0.21519068051064824 1;
	setAttr ".pm[2]" -type "matrix" -0.53897948138319685 2.2748647645294459e-16 0.84231889367857593 0
		 4.5850576870377029e-16 1 -3.4012209717910742e-17 0 -0.84231889367857593 3.9025299304062889e-16 -0.53897948138319696 0
		 -0.87102396185972364 -0.24508678907431197 1.1455635707064942 1;
	setAttr ".pm[3]" -type "matrix" 1 2.2748647645294459e-16 -3.3306690738754706e-16 0
		 -2.7577432828831654e-16 1 -3.6787618868534868e-16 0 2.2204460492503121e-16 3.9025299304062889e-16 1 0
		 2.0498726367950448 -0.24311777244319249 0.83077935395737768 1;
	setAttr ".pm[4]" -type "matrix" 0.98445291160305526 -1.2494844138911734e-16 0.17564869722337098 0
		 -2.9888027311865692e-17 1 5.905751753146853e-16 0 -0.17564869722337098 -5.8787955194382356e-16 0.98445291160305515 0
		 -0.50892549249654229 -0.27744954299944719 -0.21467557164412915 1;
	setAttr ".pm[5]" -type "matrix" 0.78762196098182069 -1.2494844138911742e-16 0.61615878357705134 0
		 -3.029368809371647e-16 1 5.0784011070673443e-16 0 -0.61615878357705134 -5.8787955194382366e-16 0.78762196098182069 0
		 -1.1759004755078901 -0.2606465721237648 -0.82755674515395339 1;
	setAttr ".pm[6]" -type "matrix" -0.88375148842456852 -0.39109030703825665 -0.25696629827290879 0
		 -0.40467928995717806 0.91445867718544516 -5.5511151231257815e-17 0 0.23498506119988463 0.10398893912800514 -0.96642036482677562 0
		 2.1531197334796288 0.66442870563984735 -0.30792959994723829 1;
	setAttr ".gm" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -0.0071249155778596029 0 0.035844891102506216 1;
	setAttr -s 7 ".ma";
	setAttr -s 7 ".dpf[0:6]"  4 4 4 4 4 4 4;
	setAttr -s 7 ".lw";
	setAttr -s 7 ".lw";
	setAttr ".mmi" yes;
	setAttr ".mi" 5;
	setAttr ".ucm" yes;
	setAttr -s 7 ".ifcl";
	setAttr -s 7 ".ifcl";
createNode tweak -n "tweak1";
	rename -uid "7F6AD79E-46D4-31A9-9769-DD98A4E7032E";
createNode objectSet -n "skinCluster1Set";
	rename -uid "AA97E3F9-4846-EF8D-1FAD-1EA103A3C3D3";
	setAttr ".ihi" 0;
	setAttr ".vo" yes;
createNode groupId -n "skinCluster1GroupId";
	rename -uid "A57FE74D-43C8-ABCB-34AC-CCBFA2C38D69";
	setAttr ".ihi" 0;
createNode groupParts -n "skinCluster1GroupParts";
	rename -uid "2598EBC7-4163-1489-B4C9-FE99FB9EEE8E";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "vtx[*]";
createNode objectSet -n "tweakSet1";
	rename -uid "94ADE26C-45D4-1F5B-0AFF-1A9DAA8E014C";
	setAttr ".ihi" 0;
	setAttr ".vo" yes;
createNode groupId -n "groupId7";
	rename -uid "00FC7AE6-4E64-9366-F34F-8C9A0776FB43";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts5";
	rename -uid "2C14EC13-4918-F869-16AC-3897FAFEF1A1";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "vtx[*]";
createNode dagPose -n "bindPose1";
	rename -uid "DC8BCFF3-401F-1133-0CF2-CCA5D0843DB0";
	setAttr -s 7 ".wm";
	setAttr -s 7 ".xm";
	setAttr ".xm[0]" -type "matrix" "xform" 1 1 1 0 0 0 0 -0.0071249157190322876
		 0 0.035844892263412476 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 -0.2411497804659582 -0.96835440471280931 0.015541934434418465 0.062409763086852789 1
		 1 1 yes;
	setAttr ".xm[1]" -type "matrix" "xform" 1 1 1 -2.0463005659114381e-34 -6.9388939039072284e-18
		 5.8980598183211441e-17 0 0.60499586237002101 -5.4643789493269423e-17 6.2450045135165055e-17 0
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 -0.040990300190284261 -0.16459951845307758 -0.23814819679152294 0.9563013281284678 1
		 1 1 yes;
	setAttr ".xm[2]" -type "matrix" "xform" 1 1 1 -9.7389390255710103e-17 2.2204460492503131e-16
		 5.3303454950868885e-17 0 0.9237004495703619 -0.038642259345448932 0.026755739172345888 0
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 0 -0.38452672679651068 0 0.92311385883820507 1
		 1 1 yes;
	setAttr ".xm[3]" -type "matrix" "xform" 1 1 1 0 0 0 0 0.93359647513805666 -0.0019690166311194801
		 -0.13330985556087782 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 0 0.87720564333091167 0 -0.48011483970858621 1
		 1 1 yes;
	setAttr ".xm[4]" -type "matrix" "xform" 1 1 1 3.9725167599868887e-16 3.6862873864507083e-17
		 3.3306690738754696e-16 0 -0.3382291200254296 0.49373414989486125 -0.15545637072181068 0
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 0.24158095427829326 0.97008581437681451 0.0057802140732669312 -0.023210868146822293 1
		 1 1 yes;
	setAttr ".xm[5]" -type "matrix" "xform" 1 1 1 0 5.5511151231257827e-17 0 0 0.91759577990630881
		 -0.016802970875682333 -0.034040153933098616 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 
		0 0.2412423881191828 0 0.97046489383931533 1 1 1 yes;
	setAttr ".xm[6]" -type "matrix" "xform" 1 1 1 4.163336342344337e-17 6.9388939039072284e-17
		 5.5511151231257827e-17 0 1.0028276782573229 0.0030837975701080711 -0.23106646510468043 0
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 -0.20260619293043491 0.95849032488967667 -0.041489661284624148 0.19627948360846265 1
		 1 1 yes;
	setAttr -s 7 ".m";
	setAttr -s 7 ".p";
	setAttr ".bp" yes;
createNode animCurveTU -n "nurbsCircle3_visibility";
	rename -uid "92124675-4214-F3F8-1202-BBB4EAF4ECC4";
	setAttr ".tan" 5;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 1 5 1 16 1 20 1 30 1;
	setAttr -s 5 ".kit[0:4]"  9 9 9 9 1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
createNode pairBlend -n "pairBlend1";
	rename -uid "F900CCC7-4A31-51B8-1FB9-02BE4EF32B69";
createNode animCurveTL -n "pairBlend1_inTranslateX1";
	rename -uid "2B93387E-417C-1ABA-411A-24AA2CB86FE7";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 -2.0498726367950439 5 -2.0498726367950439
		 16 -2.0498726367950439 20 -2.0498726367950439 30 -2.0498726367950439;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTL -n "pairBlend1_inTranslateY1";
	rename -uid "11A385FA-4E46-BA6F-D774-DCB5D3BD43B8";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0.69994072528868845 5 1.0858094863730123
		 16 0.099611538000340838 20 -0.4812183520942408 30 0.69994072528868845;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTL -n "pairBlend1_inTranslateZ1";
	rename -uid "A6382DC8-478F-6B33-EEB0-269A07F03913";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 -0.83077937364578247 5 -0.83077937364578247
		 16 -0.83077937364578247 20 -0.83077937364578247 30 -0.83077937364578247;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTA -n "pairBlend1_inRotateX1";
	rename -uid "088B3EAC-4BA1-C8C3-30B4-AD85311E0B83";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0 5 0 16 0 20 0 30 0;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTA -n "pairBlend1_inRotateY1";
	rename -uid "73BC91BF-4CED-9A46-B974-4CA459B96451";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0 5 0 16 0 20 0 30 0;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTA -n "pairBlend1_inRotateZ1";
	rename -uid "6EBFCF39-41B7-BCC1-E5BE-2FBFB4CE02FE";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 89.999999999999986 5 89.999999999999986
		 16 89.999999999999986 20 89.999999999999986 30 89.999999999999986;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTU -n "nurbsCircle3_scaleX";
	rename -uid "A03D8ECE-484A-D788-E591-91A404A34FB7";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0.60663460867497876 5 0.60663460867497876
		 16 0.60663460867497876 20 0.60663460867497876 30 0.60663460867497876;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTU -n "nurbsCircle3_scaleY";
	rename -uid "A4096519-4E84-3A94-FE6D-74A87A6CD489";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0.60663460867497876 5 0.60663460867497876
		 16 0.60663460867497876 20 0.60663460867497876 30 0.60663460867497876;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTU -n "nurbsCircle3_scaleZ";
	rename -uid "8B404697-47AD-66E1-BCDB-688A90501135";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0.60663460867497876 5 0.60663460867497876
		 16 0.60663460867497876 20 0.60663460867497876 30 0.60663460867497876;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTU -n "nurbsCircle2_visibility";
	rename -uid "CB5FA568-45E3-D265-E49C-9DAC305CE973";
	setAttr ".tan" 5;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 1 16 1 20 1 30 1;
	setAttr -s 4 ".kit[0:3]"  9 9 9 1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
createNode pairBlend -n "pairBlend2";
	rename -uid "90B9BE11-4FD4-CC3B-169E-A686B0B69CA0";
createNode animCurveTL -n "pairBlend2_inTranslateX1";
	rename -uid "46E17958-4A24-A2CF-3FB2-F686B39E8A7A";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 -1.1730272854844026 16 -1.3361895009481162
		 20 -1.2938881858278941 30 -1.1730272854844026;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTL -n "pairBlend2_inTranslateY1";
	rename -uid "C361F5E4-4D8E-9536-ED8F-ECB54F743F19";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 1.1643601058504083 16 -0.29998941228696518
		 20 0.079656759081983608 30 1.1643601058504083;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTL -n "pairBlend2_inTranslateZ1";
	rename -uid "CD8E9398-4A05-6BEC-3796-6A8F6FC28B30";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 -0.11624468117952348 16 -0.082571395852351306
		 20 -0.091301506863099652 30 -0.11624468117952348;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTA -n "pairBlend2_inRotateX1";
	rename -uid "4CCA5651-41B9-1709-4587-1A871A82A664";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0 16 0 20 0 30 0;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTA -n "pairBlend2_inRotateY1";
	rename -uid "DFF2E5B5-43EC-006F-16A4-21AD160624E7";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0 16 0 20 0 30 0;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTA -n "pairBlend2_inRotateZ1";
	rename -uid "5B12B3B3-4203-70AC-A130-D1B212D819D6";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 89.999999999999986 16 89.999999999999986
		 20 106.03062744478393 30 89.999999999999986;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTU -n "nurbsCircle2_scaleX";
	rename -uid "4A4C3F8C-4FAA-DA78-1653-E2BCCE31D955";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0.67136802995725198 16 0.67136802995725198
		 20 0.67136802995725198 30 0.67136802995725198;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTU -n "nurbsCircle2_scaleY";
	rename -uid "D4E787CE-49B0-8E76-C811-80B30DC72645";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0.67136802995725198 16 0.67136802995725198
		 20 0.67136802995725198 30 0.67136802995725198;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTU -n "nurbsCircle2_scaleZ";
	rename -uid "C3F96FBE-4FC7-214F-03A9-C5A8DDB1E9E9";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0.67136802995725198 16 0.67136802995725198
		 20 0.67136802995725198 30 0.67136802995725198;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTU -n "nurbsCircle1_visibility";
	rename -uid "D65CE5C3-4B5E-AEC2-5A75-8F948597D7B0";
	setAttr ".tan" 5;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 1 16 1 30 1;
	setAttr -s 3 ".kit[0:2]"  9 9 1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
createNode pairBlend -n "pairBlend3";
	rename -uid "528D0E32-4AC0-C8D7-ADDC-B2B710E8E2BA";
createNode animCurveTL -n "pairBlend3_inTranslateX1";
	rename -uid "4FDE1011-45F5-AC7C-97A6-DAB70E8407C7";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 -0.53704297542572021 16 -0.53704297542572021
		 30 -0.53704297542572021;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTL -n "pairBlend3_inTranslateY1";
	rename -uid "51C19DD8-407B-CE43-FC86-E48ADEC54310";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0.28372904658317566 16 0.28372904658317566
		 30 0.28372904658317566;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTL -n "pairBlend3_inTranslateZ1";
	rename -uid "99F60BAF-429F-1DAD-DDBE-07A429B2EB98";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0.10443549603223799 16 0.10443549603223799
		 30 0.10443549603223799;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTA -n "pairBlend3_inRotateX1";
	rename -uid "CAFC77E5-4496-D76F-D6D1-66AA2CC23451";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0 16 0 30 0;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTA -n "pairBlend3_inRotateY1";
	rename -uid "4C493678-47DA-4010-DB61-148C81BBC9A5";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0 16 0 30 0;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTA -n "pairBlend3_inRotateZ1";
	rename -uid "E133128B-4651-265F-66CF-D396CD323B0A";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 74.475327512298534 16 109.58441687007348
		 30 74.475327512298534;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTU -n "nurbsCircle1_scaleX";
	rename -uid "59AF6848-4408-5FBB-71E3-75B1F7D0FEFA";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0.77462092838989549 16 0.77462092838989549
		 30 0.77462092838989549;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTU -n "nurbsCircle1_scaleY";
	rename -uid "74BA69D0-4489-D8BD-3FC3-3BA657FF47F1";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0.77462092838989549 16 0.77462092838989549
		 30 0.77462092838989549;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTU -n "nurbsCircle1_scaleZ";
	rename -uid "29079EA0-460B-7DAA-847C-D5AEC983AAE6";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0.77462092838989549 16 0.77462092838989549
		 30 0.77462092838989549;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTU -n "nurbsCircle4_visibility";
	rename -uid "7B3F596B-4726-B131-2C81-B4A53C84A778";
	setAttr ".tan" 5;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 1 16 1 30 1;
	setAttr -s 3 ".kit[0:2]"  9 9 1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
createNode pairBlend -n "pairBlend4";
	rename -uid "90609C66-40D7-5CDF-B3EF-25A9CC666901";
createNode animCurveTL -n "pairBlend4_inTranslateX1";
	rename -uid "E6795464-4A9D-0334-A042-E1A38E86D231";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0.53872066736221313 16 0.53872066736221313
		 30 0.53872066736221313;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTL -n "pairBlend4_inTranslateY1";
	rename -uid "9C40A6EE-4F50-6131-1CAC-10B6E88204D3";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0.27744954824447643 16 0.27744954824447643
		 30 0.27744954824447643;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTL -n "pairBlend4_inTranslateZ1";
	rename -uid "F2D5D5FC-46E8-5843-0611-1686E3D9823A";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0.12194589525461197 16 0.12194589525461197
		 30 0.12194589525461197;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTA -n "pairBlend4_inRotateX1";
	rename -uid "047477E9-4FF8-715C-CD23-BFB5989D114D";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0 16 0 30 0;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTA -n "pairBlend4_inRotateY1";
	rename -uid "022AA7DB-4633-6C99-6F6C-F0A59364ABB8";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0 16 0 30 0;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTA -n "pairBlend4_inRotateZ1";
	rename -uid "47B15719-4A55-5320-C14B-18B70B57C446";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 100.46429200643412 16 70.878796835223369
		 30 100.46429200643412;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTU -n "nurbsCircle4_scaleX";
	rename -uid "30A00BE1-4BF9-96B3-3701-BCBDA8B3FFD9";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0.77462092838989549 16 0.77462092838989549
		 30 0.77462092838989549;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTU -n "nurbsCircle4_scaleY";
	rename -uid "1A9FBB89-4965-FBD0-B0DC-798408A99E95";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0.77462092838989549 16 0.77462092838989549
		 30 0.77462092838989549;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTU -n "nurbsCircle4_scaleZ";
	rename -uid "1F340127-4495-16B6-2DC9-F9A9BAC917E7";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0.77462092838989549 16 0.77462092838989549
		 30 0.77462092838989549;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTU -n "nurbsCircle5_visibility";
	rename -uid "5935C0B4-4CF2-D290-E237-619585E95AB6";
	setAttr ".tan" 5;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 1 16 1 20 1 30 1;
	setAttr -s 4 ".kit[0:3]"  9 9 9 1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
createNode pairBlend -n "pairBlend5";
	rename -uid "3D3CF797-4663-F96D-927A-E8826ACD894F";
createNode animCurveTL -n "pairBlend5_inTranslateX1";
	rename -uid "7F424497-4EB7-DEDA-2968-51B03C7D7086";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 1.1440959396576396 16 1.2786059514449883
		 20 1.2437329854260462 30 1.1440959396576396;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTL -n "pairBlend5_inTranslateY1";
	rename -uid "D3FBF818-406A-C93F-F480-B8B5A0BC87DD";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 1.2056652105371801 16 -0.2753763905513622
		 20 0.10859735787900077 30 1.2056652105371801;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTL -n "pairBlend5_inTranslateZ1";
	rename -uid "C6AFA7B7-430A-BF4F-03A1-679B806D5467";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 -0.072739541530609131 16 -0.072739541530609131
		 20 -0.072739541530609131 30 -0.072739541530609131;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTA -n "pairBlend5_inRotateX1";
	rename -uid "A1DDCEE1-4EA0-E311-D796-6F9635C8A2E2";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0 16 0 20 0 30 0;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTA -n "pairBlend5_inRotateY1";
	rename -uid "CCB328C6-4A0D-ED39-D23C-FA9B14CB260C";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0 16 0 20 0 30 0;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTA -n "pairBlend5_inRotateZ1";
	rename -uid "D23D2B74-4F79-5FEA-02E2-77A7EB79694C";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 89.999999999999986 16 89.999999999999986
		 20 76.403179736120165 30 89.999999999999986;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTU -n "nurbsCircle5_scaleX";
	rename -uid "7E7D40B2-420C-71D2-DD95-AAAC511F220C";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0.67136802995725198 16 0.67136802995725198
		 20 0.67136802995725198 30 0.67136802995725198;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTU -n "nurbsCircle5_scaleY";
	rename -uid "435A482F-42DA-7C74-EF88-6AACFF5A53A5";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0.67136802995725198 16 0.67136802995725198
		 20 0.67136802995725198 30 0.67136802995725198;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTU -n "nurbsCircle5_scaleZ";
	rename -uid "9B755B8C-416A-5478-2C35-4F9D6D409C09";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0.67136802995725198 16 0.67136802995725198
		 20 0.67136802995725198 30 0.67136802995725198;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTU -n "nurbsCircle6_visibility";
	rename -uid "DA1AA654-46FD-C8A5-C884-95B5B2030015";
	setAttr ".tan" 5;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 1 5 1 16 1 20 1 30 1;
	setAttr -s 5 ".kit[0:4]"  9 9 9 9 1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
createNode pairBlend -n "pairBlend6";
	rename -uid "96BAF48E-4B73-2BC3-D467-7B9C0A9359AA";
createNode animCurveTL -n "pairBlend6_inTranslateX1";
	rename -uid "5137EA84-4934-8A01-1C67-55ACC99DB799";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 2.0835468769073486 5 2.0835468769073486
		 16 2.0835468769073486 20 2.0835468769073486 30 2.0835468769073486;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTL -n "pairBlend6_inTranslateY1";
	rename -uid "DBB5B47D-478E-8C08-F224-A8BBA6BF5AFE";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0.64796598422268969 5 1.2695123393147165
		 16 0.053344406147080692 20 -0.36573724404511426 30 0.64796598422268969;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTL -n "pairBlend6_inTranslateZ1";
	rename -uid "77C5F205-4F7D-D4DE-7E19-D8B6CAEBF0F5";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 -0.87263363599777211 5 -0.87263363599777211
		 16 -0.87263363599777211 20 -0.87263363599777211 30 -0.87263363599777211;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTA -n "pairBlend6_inRotateX1";
	rename -uid "0ABF9C60-4B0E-9F89-A5F9-1583496C49DB";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0 5 0 16 0 20 0 30 0;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTA -n "pairBlend6_inRotateY1";
	rename -uid "8C27364E-4B06-70BF-5290-F3A3F0AFAF13";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0 5 0 16 0 20 0 30 0;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTA -n "pairBlend6_inRotateZ1";
	rename -uid "DE38D18F-41B6-5998-4686-DD8DC7342A97";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 89.999999999999986 5 89.999999999999986
		 16 89.999999999999986 20 89.999999999999986 30 89.999999999999986;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTU -n "nurbsCircle6_scaleX";
	rename -uid "CEC2F093-46D3-B299-2133-6CAC3586D535";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0.60663460867497876 5 0.60663460867497876
		 16 0.60663460867497876 20 0.60663460867497876 30 0.60663460867497876;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTU -n "nurbsCircle6_scaleY";
	rename -uid "D5002F62-4C04-417C-79B2-8C87235533B9";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0.60663460867497876 5 0.60663460867497876
		 16 0.60663460867497876 20 0.60663460867497876 30 0.60663460867497876;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTU -n "nurbsCircle6_scaleZ";
	rename -uid "3AEC05B2-4AB7-3903-3030-CCB919B9EB38";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0.60663460867497876 5 0.60663460867497876
		 16 0.60663460867497876 20 0.60663460867497876 30 0.60663460867497876;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTU -n "nurbsCircle3_blendParent1";
	rename -uid "5FAEA09D-4F22-B1EC-8DBD-D68C434C3EA3";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0 5 0 16 0 20 0 30 0;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode animCurveTU -n "nurbsCircle2_blendParent1";
	rename -uid "27E10F9A-4A28-F992-5772-67B0A45F7F0A";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0 16 0 20 0 30 0;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTU -n "nurbsCircle1_blendParent1";
	rename -uid "B5524FE8-4A0F-F69C-92E4-C8A31B84E25F";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0 16 0 30 0;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTU -n "nurbsCircle4_blendParent1";
	rename -uid "3CB1BEB2-44C8-BEF3-E6C7-B286BF6E8607";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0 16 0 30 0;
	setAttr -s 3 ".kit[2]"  1;
	setAttr -s 3 ".kot[2]"  1;
	setAttr -s 3 ".kix[2]"  1;
	setAttr -s 3 ".kiy[2]"  0;
	setAttr -s 3 ".kox[2]"  1;
	setAttr -s 3 ".koy[2]"  0;
createNode animCurveTU -n "nurbsCircle5_blendParent1";
	rename -uid "0471C781-4FE5-AEB9-897B-54A94583DE6E";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0 16 0 20 0 30 0;
	setAttr -s 4 ".kit[3]"  1;
	setAttr -s 4 ".kot[3]"  1;
	setAttr -s 4 ".kix[3]"  1;
	setAttr -s 4 ".kiy[3]"  0;
	setAttr -s 4 ".kox[3]"  1;
	setAttr -s 4 ".koy[3]"  0;
createNode animCurveTU -n "nurbsCircle6_blendParent1";
	rename -uid "E7D4EE52-43E2-991B-6DD1-14A632D0F8E6";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  1 0 5 0 16 0 20 0 30 0;
	setAttr -s 5 ".kit[4]"  1;
	setAttr -s 5 ".kot[4]"  1;
	setAttr -s 5 ".kix[4]"  1;
	setAttr -s 5 ".kiy[4]"  0;
	setAttr -s 5 ".kox[4]"  1;
	setAttr -s 5 ".koy[4]"  0;
createNode script -n "uiConfigurationScriptNode";
	rename -uid "262CF426-4C5A-9591-10FB-B4BE50025EEF";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n"
		+ "            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n"
		+ "            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n"
		+ "            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n"
		+ "            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n"
		+ "            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n"
		+ "            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n"
		+ "            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n"
		+ "            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n"
		+ "            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 1\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n"
		+ "            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n"
		+ "            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1563\n            -height 716\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"ToggledOutliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"ToggledOutliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n"
		+ "            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -isSet 0\n            -isSetMember 0\n"
		+ "            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            -renderFilterIndex 0\n            -selectionOrder \"chronological\" \n            -expandAttribute 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 0\n            -showReferenceMembers 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n"
		+ "            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n"
		+ "                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n"
		+ "                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 1\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1.25\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -showCurveNames 0\n"
		+ "                -showActiveCurveNames 0\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                -valueLinesToggle 1\n                -outliner \"graphEditor1OutlineEd\" \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n"
		+ "                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n"
		+ "                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n"
		+ "                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"timeEditorPanel\" (localizedPanelLabel(\"Time Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Time Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n"
		+ "                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n"
		+ "                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Editor\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"shapePanel\" (localizedPanelLabel(\"Shape Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tshapePanel -edit -l (localizedPanelLabel(\"Shape Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"posePanel\" (localizedPanelLabel(\"Pose Editor\")) `;\n\tif (\"\" != $panelName) {\n"
		+ "\t\t$label = `panel -q -label $panelName`;\n\t\tposePanel -edit -l (localizedPanelLabel(\"Pose Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"profilerPanel\" (localizedPanelLabel(\"Profiler Tool\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"contentBrowserPanel\" (localizedPanelLabel(\"Content Browser\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Content Browser\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -connectNodeOnCreation 0\n                -connectOnDrop 0\n                -highlightConnections 0\n                -copyConnectionsOnPaste 0\n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -crosshairOnEdgeDragging 0\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n"
		+ "                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -activeTab -1\n                -editorMode \"default\" \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-userCreated false\n\t\t\t\t-defaultImage \"vacantCell.xP:/\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 32768\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 1563\\n    -height 716\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 32768\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 1563\\n    -height 716\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 12 -divisions 5 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	rename -uid "19EE2AC7-4AA5-1635-0722-BB88749F0BBF";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 30 -ast 1 -aet 250 ";
	setAttr ".st" 6;
createNode polyPlanarProj -n "polyPlanarProj1";
	rename -uid "61F0E55E-49FF-9EBD-4604-D9BE026FD121";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:133]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -0.0071249155778596029 0 0.035844891102506216 1;
	setAttr ".ws" yes;
	setAttr ".pc" -type "double3" -0.23115637898445129 -0.1438022255897522 -0.54692137241363525 ;
	setAttr ".ro" -type "double3" -45.338350058536278 61.799999254227878 1.2517321104534771e-06 ;
	setAttr ".ps" -type "double2" 3.3819043728344242 2.7587739182824365 ;
	setAttr ".per" yes;
	setAttr ".cam" -type "matrix" 0.91884869337081909 -2.5637404918670654 -0.61949712038040161 -0.61948472261428833
		 -2.3777940659058241e-16 2.8748760223388672 -0.71128439903259277 -0.71127015352249146
		 -1.7136455774307251 -1.3746654987335205 -0.33217144012451172 -0.33216479420661926
		 -0.12106231600046158 -0.2533886730670929 7.4794521331787109 7.6793003082275391;
	setAttr ".prgt" 1119;
	setAttr ".ptop" 532;
createNode polyMapCut -n "polyMapCut1";
	rename -uid "AE2B095E-4C7A-D31E-F65C-2BA4A84A32CB";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "e[224:263]" "e[265:266]";
createNode polyTweakUV -n "polyTweakUV1";
	rename -uid "9B39DCED-4158-75CF-A5B1-52923A091E8F";
	setAttr ".uopa" yes;
	setAttr -s 177 ".uvtk[0:176]" -type "float2" 0.96964324 0.049060583 0.91818285
		 0.02307719 0.0038909577 0.64137733 0.014357787 0.6749059 0.074931808 0.049441494
		 0.073994301 -0.046770737 0.56583065 -0.10040081 0.57847548 0.01373828 -0.086535498
		 0.57505798 -0.081820592 0.44589669 -0.18321025 0.26388055 -0.20102623 0.38038915
		 -0.21503496 -0.37879428 -0.17042601 -0.25272536 0.066280954 -0.60497046 -0.16958597
		 -0.63107288 -0.068631634 0.58725083 -0.063410565 0.50498343 -0.093056813 0.39638382
		 -0.075979158 0.45997781 0.65306604 -0.059371829 0.66515011 0.023279138 -0.029382579
		 0.63459396 -0.025526717 0.58715284 -0.037968569 0.55065751 0.7811532 -0.014061302
		 0.82837206 0.030837771 -0.089266554 0.35102284 -0.16730088 0.19502407 -0.17897481
		 0.18921548 -0.15668511 0.15171599 0.11691345 -0.12273893 0.52287233 -0.16099846 -0.097560987
		 0.32779318 -0.16684103 0.17651811 -0.088308677 0.32571429 -0.075223476 0.10846552
		 -0.16004457 -0.039462343 0.11235956 -0.14249605 -0.15088296 0.14049011 -0.15308352
		 -0.075593293 0.07534381 -0.38871419 0.51537162 -0.17980614 0.46642238 -0.42267427
		 -0.077812798 0.083810866 -0.095398739 0.30768985 -0.16331226 -0.084496289 -0.081141755
		 0.061846569 -0.16335186 -0.20993897 -0.2099748 -0.33256266 0.054407615 -0.43479326
		 -0.15571067 -0.12211695 0.43646574 -0.46411502 -0.083513185 0.031750981 -0.045545984
		 0.083914116 -0.03953629 0.016015263 -0.20378193 0.18404222 -0.20776024 0.24071172
		 -0.2258195 0.31353486 -0.26085815 0.19476044 -0.19694126 0.074295297 -0.24911425
		 0.17848825 -0.27403602 0.19701687 0.0024938621 0.68489933 0.92329448 0.074974328
		 0.57674748 0.15454939 0.07611043 0.18725649 -0.042909641 0.17890206 -0.26572075 0.26888132
		 -0.21422645 0.45415747 -0.10487814 0.64848936 -0.37683275 0.97836971 -0.4465481 0.88455522
		 -0.24208802 0.8875342 0.0033936612 0.83086222 -0.076815054 0.64448416 -0.075236946
		 0.64676213 -0.10450825 0.65111089 0.661771 0.12349319 -0.034606654 0.67092037 -0.032822136
		 0.66663647 0.78444642 0.083572164 -0.2125037 0.50504315 -0.12196994 0.67362785 -0.20805904
		 0.47348458 -0.20037064 0.43731785 0.11176915 0.25719011 0.53421646 0.23783401 -0.12742637
		 0.67541087 -0.12533587 0.69059014 -0.21746668 0.52159595 -0.28180614 0.73482025 -0.17653698
		 0.8999722 -0.28925946 0.7147646 -0.21630839 0.48938972 0.10783613 0.27935457 0.04914169
		 0.56464845 0.52839565 0.26152611 0.47029835 0.55324268 -0.13247316 0.69102538 -0.18486288
		 0.91342902 -0.19967449 0.94193983 -0.30452916 0.78224409 -0.43654406 0.89608359 -0.36246684
		 0.95488536 -0.30830193 0.75356388 0.015770022 0.63390315 0.42854089 0.61952782 -0.2056725
		 0.96051586 -0.22971544 0.36392587 -0.23527786 0.37597042 -0.27527151 0.25210026 0.83494306
		 0.0055862116 0.88710761 0.039277427 -0.023533598 0.59463072 -0.0062705074 0.65881789
		 -0.014238209 0.61674047 -0.018481962 0.67266655 -0.017182581 0.66904879 0.84154183
		 0.076914445 0.015691388 0.67657995 0.0050794147 0.64432096 0.0022096559 0.68528092
		 -0.29088613 0.76043987 -0.18430179 0.92625332 -0.19130373 0.94227231 0.46076351 0.58792734
		 0.039168537 0.59895015 -0.2987121 0.74093616 -0.15245225 -0.092322975 0.071517192
		 -0.40729463 0.46223122 -0.4405849 -0.077267595 0.065057978 -0.07523337 0.091606289
		 -0.16005841 -0.055905707 -0.25367993 0.13839301 -0.23739412 0.16919872 -0.20963103
		 0.20164198 -0.060061473 0.24487457 0.056013767 0.27851546 0.076128669 0.30904043
		 0.072626047 0.32991242 0.016161922 0.60639852 0.001855392 0.63832432 -0.017996904
		 0.6579777 -0.26867014 0.92341453 0.0028509609 0.87702841 0.44550651 0.66196513 0.48057926
		 0.62714314 0.4941504 0.58865148 0.55920839 0.29316849 0.56701958 0.26962078 0.62248474
		 0.21937144 0.69701689 0.16569248 0.82281119 0.11486024 0.87379003 0.092433751 0.9424336
		 0.076338947 0.97172099 0.05030413 0.93853503 0.025091939 0.85944116 0.0067995703
		 0.80624771 -0.017190561 0.66555023 -0.066924438 0.58293241 -0.11410455 0.53902435
		 -0.16534057 0.53093141 -0.18622065 0.47751921 -0.43486139 0.46982884 -0.45209906
		 0.44028074 -0.47619426 0.056555841 -0.61898601 -0.22318795 -0.62787974 0.016920466
		 -0.41000727 0.034841496 -0.38782102 0.039277524 -0.37198982 0.080775611 -0.11169426
		 0.075387798 -0.093987718 0.028154764 -0.031363457 -0.064800277 0.015060814;
createNode lambert -n "lambert2";
	rename -uid "504CAC02-4B52-CC7B-90A1-EA98817F3FE3";
createNode shadingEngine -n "lambert2SG";
	rename -uid "152971A5-46E6-9AD6-6DF6-07ABCB52F1A3";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo1";
	rename -uid "7B439EF7-44F7-BAD5-0604-04B028DF8D07";
createNode file -n "file1";
	rename -uid "C2783317-4864-3A9D-6B3E-BDA327E760EF";
	setAttr ".ftn" -type "string" "D:/Maya/Mouette projet/sourceimages/uvmouette.jpg";
	setAttr ".cs" -type "string" "sRGB";
createNode place2dTexture -n "place2dTexture1";
	rename -uid "074A160C-4A03-3DB7-8134-BDABEC173FB4";
createNode VRaySettingsNode -s -n "vraySettings";
	rename -uid "FC7CDFA4-40D2-C265-6881-E4BD2CC00F80";
	setAttr ".pe" 2;
	setAttr ".se" 3;
	setAttr ".cmph" 60;
	setAttr ".cfile" -type "string" "";
	setAttr ".cfile2" -type "string" "";
	setAttr ".casf" -type "string" "";
	setAttr ".casf2" -type "string" "";
	setAttr ".st" 3;
	setAttr ".msr" 6;
	setAttr ".aaft" 3;
	setAttr ".aafs" 2;
	setAttr ".dma" 24;
	setAttr ".dam" 1;
	setAttr ".pt" 0.0099999997764825821;
	setAttr ".sd" 1000;
	setAttr ".ss" 0.01;
	setAttr ".pfts" 20;
	setAttr ".ufg" yes;
	setAttr ".fnm" -type "string" "";
	setAttr ".lcfnm" -type "string" "";
	setAttr ".asf" -type "string" "";
	setAttr ".lcasf" -type "string" "";
	setAttr ".urtrshd" yes;
	setAttr ".rtrshd" 2;
	setAttr ".ifile" -type "string" "";
	setAttr ".ifile2" -type "string" "";
	setAttr ".iasf" -type "string" "";
	setAttr ".iasf2" -type "string" "";
	setAttr ".pmfile" -type "string" "";
	setAttr ".pmfile2" -type "string" "";
	setAttr ".pmasf" -type "string" "";
	setAttr ".pmasf2" -type "string" "";
	setAttr ".dmcstd" yes;
	setAttr ".dmculs" no;
	setAttr ".dmcsat" 0.004999999888241291;
	setAttr ".cmtp" 6;
	setAttr ".cmao" 2;
	setAttr ".cg" 2.2000000476837158;
	setAttr ".mtah" yes;
	setAttr ".rgbcs" 1;
	setAttr ".srflc" 1;
	setAttr ".seu" yes;
	setAttr ".gormio" yes;
	setAttr ".gopl" 2;
	setAttr ".wi" 960;
	setAttr ".he" 540;
	setAttr ".aspr" 1.7777780294418335;
	setAttr ".autolt" 0;
	setAttr ".jpegq" 100;
	setAttr ".vfbOn" yes;
	setAttr ".vfbSA" -type "Int32Array" 256 1018 17 610 265 996 649
		 610 265 810 440 -1073724351 1 835 240 0 -1920 -1080 960
		 540 1023 0 -1073741824 0 -1073741824 0 1072693248 0 1072693248 1 0
		 0 894 1 3 1 0 0 0 0 1 0 5
		 0 1065353216 3 1 0 0 0 0 1 0 5 0
		 1065353216 3 1 1065353216 0 0 0 1 0 5 0 1065353216
		 1 3 2 1065353216 1065353216 1065353216 1065353216 1 0 5 0 0
		 0 0 1 0 5 0 1065353216 1 137531 65536 1 1313131313
		 65536 944879383 0 -525502228 1065353216 1621981420 1034594987 1057896675 1065353216 2 0 0
		 -1097805629 -1097805629 1049678019 1049678019 0 2 1065353216 1065353216 -1097805629 -1097805629 1049678019 1049678019
		 0 2 1 2 -1 0 0 0 1869047123 1230315621 -1362728192 575
		 -1362737872 575 -1362743248 575 -1362738320 575 -1362735184 575 -1362725328 575 -1362754896 575
		 -1249342688 575 -1362721744 575 -1362760720 575 -1362745488 575 -1249383456 16777215 0 70
		 1 32 53 1632775510 1868963961 1632444530 622879097 2036429430 1936876918 544108393 1701978236 1919247470
		 1835627552 1915035749 1701080677 1835627634 12901 1378702848 1713404257 1293972079 543258977 808857139 540291118 1701978236
		 1919247470 1835627552 807411813 807411816 857743469 7550254 16777216 16777216 0 0 0 0
		 1 1 0 0 0 0 1 2 0 0 0 11
		 1936614732 1701209669 7566435 1 0 1 0 1097859072 1097859072 1082130432 0 0
		 0 1077936128 0 0 0 1 2 1 1106247680 1092616192 1 0
		 0 0 0 82176 0 16576 16752 0 0 0 16448 0
		 65536 65536 65536 0 0 0 65536 0 0 0 0 0
		 0 0 0 0 0 0 0 65536 536870912 536888779 ;
	setAttr ".mSceneName" -type "string" "I:/CLASSES/DEUXIEME ANNEE/COMMUN/Projet Pirate/Graph/Ressources graphiques VALIDEE/Element secondaire/Mouette.ma";
	setAttr ".rt_maxPaths" 10000;
	setAttr ".rt_engineType" 3;
createNode renderSetup -n "renderSetup";
	rename -uid "8760CE91-4D9A-0EE6-807C-C4974EBE8661";
createNode nodeGraphEditorInfo -n "hyperShadePrimaryNodeEditorSavedTabsInfo";
	rename -uid "7E8C16D8-4597-1660-6C69-51A04027B57B";
	setAttr ".tgi[0].tn" -type "string" "Untitled_1";
	setAttr ".tgi[0].vl" -type "double2" -554.16664464606265 103.57142445586996 ;
	setAttr ".tgi[0].vh" -type "double2" -341.07141501846826 529.76188371105889 ;
	setAttr -s 4 ".tgi[0].ni";
	setAttr ".tgi[0].ni[0].x" 367.14285278320313;
	setAttr ".tgi[0].ni[0].y" 120;
	setAttr ".tgi[0].ni[0].nvs" 1923;
	setAttr ".tgi[0].ni[1].x" 60;
	setAttr ".tgi[0].ni[1].y" 142.85714721679688;
	setAttr ".tgi[0].ni[1].nvs" 1923;
	setAttr ".tgi[0].ni[2].x" -554.28570556640625;
	setAttr ".tgi[0].ni[2].y" 120;
	setAttr ".tgi[0].ni[2].nvs" 1923;
	setAttr ".tgi[0].ni[3].x" -247.14285278320313;
	setAttr ".tgi[0].ni[3].y" 142.85714721679688;
	setAttr ".tgi[0].ni[3].nvs" 1923;
select -ne :time1;
	setAttr ".o" 19;
	setAttr ".unw" 19;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
	setAttr ".fprt" yes;
select -ne :renderPartition;
	setAttr -s 3 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 5 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderUtilityList1;
select -ne :defaultRenderingList1;
select -ne :defaultTextureList1;
select -ne :initialShadingGroup;
	setAttr -s 5 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 4 ".gn";
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultRenderGlobals;
	setAttr ".ren" -type "string" "vray";
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :ikSystem;
	setAttr -s 4 ".sol";
connectAttr "groupId3.id" "pCubeShape1.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCubeShape1.iog.og[0].gco";
connectAttr "groupParts2.og" "pCubeShape1.i";
connectAttr "groupId4.id" "pCubeShape1.ciog.cog[0].cgid";
connectAttr "groupId1.id" "pCubeShape2.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCubeShape2.iog.og[0].gco";
connectAttr "groupParts1.og" "pCubeShape2.i";
connectAttr "groupId2.id" "pCubeShape2.ciog.cog[0].cgid";
connectAttr "polyTweakUV1.out" "pCube3Shape.i";
connectAttr "skinCluster1GroupId.id" "pCube3Shape.iog.og[7].gid";
connectAttr "skinCluster1Set.mwc" "pCube3Shape.iog.og[7].gco";
connectAttr "groupId7.id" "pCube3Shape.iog.og[8].gid";
connectAttr "tweakSet1.mwc" "pCube3Shape.iog.og[8].gco";
connectAttr "tweak1.vl[0].vt[0]" "pCube3Shape.twl";
connectAttr "polyTweakUV1.uvtk[0]" "pCube3Shape.uvst[0].uvtw";
connectAttr "polySoftEdge2.out" "pCube3ShapeOrig.i";
connectAttr "joint1_parentConstraint1.ctx" "joint1.tx";
connectAttr "joint1_parentConstraint1.cty" "joint1.ty";
connectAttr "joint1_parentConstraint1.ctz" "joint1.tz";
connectAttr "joint1_parentConstraint1.crx" "joint1.rx";
connectAttr "joint1_parentConstraint1.cry" "joint1.ry";
connectAttr "joint1_parentConstraint1.crz" "joint1.rz";
connectAttr "joint1.s" "joint2.is";
connectAttr "joint2_parentConstraint1.ctx" "joint2.tx";
connectAttr "joint2_parentConstraint1.cty" "joint2.ty";
connectAttr "joint2_parentConstraint1.ctz" "joint2.tz";
connectAttr "joint2_parentConstraint1.crx" "joint2.rx";
connectAttr "joint2_parentConstraint1.cry" "joint2.ry";
connectAttr "joint2_parentConstraint1.crz" "joint2.rz";
connectAttr "joint2.s" "joint3.is";
connectAttr "joint3_parentConstraint1.ctx" "joint3.tx";
connectAttr "joint3_parentConstraint1.cty" "joint3.ty";
connectAttr "joint3_parentConstraint1.ctz" "joint3.tz";
connectAttr "joint3_parentConstraint1.crx" "joint3.rx";
connectAttr "joint3_parentConstraint1.cry" "joint3.ry";
connectAttr "joint3_parentConstraint1.crz" "joint3.rz";
connectAttr "joint3.s" "joint4.is";
connectAttr "joint4_parentConstraint1.ctx" "joint4.tx";
connectAttr "joint4_parentConstraint1.cty" "joint4.ty";
connectAttr "joint4_parentConstraint1.ctz" "joint4.tz";
connectAttr "joint4_parentConstraint1.crx" "joint4.rx";
connectAttr "joint4_parentConstraint1.cry" "joint4.ry";
connectAttr "joint4_parentConstraint1.crz" "joint4.rz";
connectAttr "joint4.ro" "joint4_parentConstraint1.cro";
connectAttr "joint4.pim" "joint4_parentConstraint1.cpim";
connectAttr "joint4.rp" "joint4_parentConstraint1.crp";
connectAttr "joint4.rpt" "joint4_parentConstraint1.crt";
connectAttr "joint4.jo" "joint4_parentConstraint1.cjo";
connectAttr "nurbsCircle3.t" "joint4_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle3.rp" "joint4_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle3.rpt" "joint4_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle3.r" "joint4_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle3.ro" "joint4_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle3.s" "joint4_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle3.pm" "joint4_parentConstraint1.tg[0].tpm";
connectAttr "joint4_parentConstraint1.w0" "joint4_parentConstraint1.tg[0].tw";
connectAttr "joint3.ro" "joint3_parentConstraint1.cro";
connectAttr "joint3.pim" "joint3_parentConstraint1.cpim";
connectAttr "joint3.rp" "joint3_parentConstraint1.crp";
connectAttr "joint3.rpt" "joint3_parentConstraint1.crt";
connectAttr "joint3.jo" "joint3_parentConstraint1.cjo";
connectAttr "nurbsCircle2.t" "joint3_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle2.rp" "joint3_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle2.rpt" "joint3_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle2.r" "joint3_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle2.ro" "joint3_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle2.s" "joint3_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle2.pm" "joint3_parentConstraint1.tg[0].tpm";
connectAttr "joint3_parentConstraint1.w0" "joint3_parentConstraint1.tg[0].tw";
connectAttr "joint2.ro" "joint2_parentConstraint1.cro";
connectAttr "joint2.pim" "joint2_parentConstraint1.cpim";
connectAttr "joint2.rp" "joint2_parentConstraint1.crp";
connectAttr "joint2.rpt" "joint2_parentConstraint1.crt";
connectAttr "joint2.jo" "joint2_parentConstraint1.cjo";
connectAttr "nurbsCircle1.t" "joint2_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle1.rp" "joint2_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle1.rpt" "joint2_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle1.r" "joint2_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle1.ro" "joint2_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle1.s" "joint2_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle1.pm" "joint2_parentConstraint1.tg[0].tpm";
connectAttr "joint2_parentConstraint1.w0" "joint2_parentConstraint1.tg[0].tw";
connectAttr "joint1.s" "joint5.is";
connectAttr "joint5_parentConstraint1.ctx" "joint5.tx";
connectAttr "joint5_parentConstraint1.cty" "joint5.ty";
connectAttr "joint5_parentConstraint1.ctz" "joint5.tz";
connectAttr "joint5_parentConstraint1.crx" "joint5.rx";
connectAttr "joint5_parentConstraint1.cry" "joint5.ry";
connectAttr "joint5_parentConstraint1.crz" "joint5.rz";
connectAttr "joint5.s" "joint6.is";
connectAttr "joint6_parentConstraint1.ctx" "joint6.tx";
connectAttr "joint6_parentConstraint1.cty" "joint6.ty";
connectAttr "joint6_parentConstraint1.ctz" "joint6.tz";
connectAttr "joint6_parentConstraint1.crx" "joint6.rx";
connectAttr "joint6_parentConstraint1.cry" "joint6.ry";
connectAttr "joint6_parentConstraint1.crz" "joint6.rz";
connectAttr "joint6.s" "joint7.is";
connectAttr "joint7_parentConstraint1.ctx" "joint7.tx";
connectAttr "joint7_parentConstraint1.cty" "joint7.ty";
connectAttr "joint7_parentConstraint1.ctz" "joint7.tz";
connectAttr "joint7_parentConstraint1.crx" "joint7.rx";
connectAttr "joint7_parentConstraint1.cry" "joint7.ry";
connectAttr "joint7_parentConstraint1.crz" "joint7.rz";
connectAttr "joint7.ro" "joint7_parentConstraint1.cro";
connectAttr "joint7.pim" "joint7_parentConstraint1.cpim";
connectAttr "joint7.rp" "joint7_parentConstraint1.crp";
connectAttr "joint7.rpt" "joint7_parentConstraint1.crt";
connectAttr "joint7.jo" "joint7_parentConstraint1.cjo";
connectAttr "nurbsCircle6.t" "joint7_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle6.rp" "joint7_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle6.rpt" "joint7_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle6.r" "joint7_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle6.ro" "joint7_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle6.s" "joint7_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle6.pm" "joint7_parentConstraint1.tg[0].tpm";
connectAttr "joint7_parentConstraint1.w0" "joint7_parentConstraint1.tg[0].tw";
connectAttr "joint6.ro" "joint6_parentConstraint1.cro";
connectAttr "joint6.pim" "joint6_parentConstraint1.cpim";
connectAttr "joint6.rp" "joint6_parentConstraint1.crp";
connectAttr "joint6.rpt" "joint6_parentConstraint1.crt";
connectAttr "joint6.jo" "joint6_parentConstraint1.cjo";
connectAttr "nurbsCircle5.t" "joint6_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle5.rp" "joint6_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle5.rpt" "joint6_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle5.r" "joint6_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle5.ro" "joint6_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle5.s" "joint6_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle5.pm" "joint6_parentConstraint1.tg[0].tpm";
connectAttr "joint6_parentConstraint1.w0" "joint6_parentConstraint1.tg[0].tw";
connectAttr "joint5.ro" "joint5_parentConstraint1.cro";
connectAttr "joint5.pim" "joint5_parentConstraint1.cpim";
connectAttr "joint5.rp" "joint5_parentConstraint1.crp";
connectAttr "joint5.rpt" "joint5_parentConstraint1.crt";
connectAttr "joint5.jo" "joint5_parentConstraint1.cjo";
connectAttr "nurbsCircle4.t" "joint5_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle4.rp" "joint5_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle4.rpt" "joint5_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle4.r" "joint5_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle4.ro" "joint5_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle4.s" "joint5_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle4.pm" "joint5_parentConstraint1.tg[0].tpm";
connectAttr "joint5_parentConstraint1.w0" "joint5_parentConstraint1.tg[0].tw";
connectAttr "joint1.ro" "joint1_parentConstraint1.cro";
connectAttr "joint1.pim" "joint1_parentConstraint1.cpim";
connectAttr "joint1.rp" "joint1_parentConstraint1.crp";
connectAttr "joint1.rpt" "joint1_parentConstraint1.crt";
connectAttr "joint1.jo" "joint1_parentConstraint1.cjo";
connectAttr "nurbsCircle7.t" "joint1_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle7.rp" "joint1_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle7.rpt" "joint1_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle7.r" "joint1_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle7.ro" "joint1_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle7.s" "joint1_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle7.pm" "joint1_parentConstraint1.tg[0].tpm";
connectAttr "joint1_parentConstraint1.w0" "joint1_parentConstraint1.tg[0].tw";
connectAttr "pairBlend3.otx" "nurbsCircle1.tx";
connectAttr "pairBlend3.oty" "nurbsCircle1.ty";
connectAttr "pairBlend3.otz" "nurbsCircle1.tz";
connectAttr "pairBlend3.orx" "nurbsCircle1.rx";
connectAttr "pairBlend3.ory" "nurbsCircle1.ry";
connectAttr "pairBlend3.orz" "nurbsCircle1.rz";
connectAttr "nurbsCircle1_scaleX.o" "nurbsCircle1.sx";
connectAttr "nurbsCircle1_scaleY.o" "nurbsCircle1.sy";
connectAttr "nurbsCircle1_scaleZ.o" "nurbsCircle1.sz";
connectAttr "nurbsCircle1_visibility.o" "nurbsCircle1.v";
connectAttr "nurbsCircle1_blendParent1.o" "nurbsCircle1.blendParent1";
connectAttr "makeNurbCircle1.oc" "nurbsCircleShape1.cr";
connectAttr "nurbsCircle1.ro" "nurbsCircle1_parentConstraint1.cro";
connectAttr "nurbsCircle1.pim" "nurbsCircle1_parentConstraint1.cpim";
connectAttr "nurbsCircle1.rp" "nurbsCircle1_parentConstraint1.crp";
connectAttr "nurbsCircle1.rpt" "nurbsCircle1_parentConstraint1.crt";
connectAttr "nurbsCircle7.t" "nurbsCircle1_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle7.rp" "nurbsCircle1_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle7.rpt" "nurbsCircle1_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle7.r" "nurbsCircle1_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle7.ro" "nurbsCircle1_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle7.s" "nurbsCircle1_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle7.pm" "nurbsCircle1_parentConstraint1.tg[0].tpm";
connectAttr "nurbsCircle1_parentConstraint1.w0" "nurbsCircle1_parentConstraint1.tg[0].tw"
		;
connectAttr "pairBlend2.otx" "nurbsCircle2.tx";
connectAttr "pairBlend2.oty" "nurbsCircle2.ty";
connectAttr "pairBlend2.otz" "nurbsCircle2.tz";
connectAttr "pairBlend2.orx" "nurbsCircle2.rx";
connectAttr "pairBlend2.ory" "nurbsCircle2.ry";
connectAttr "pairBlend2.orz" "nurbsCircle2.rz";
connectAttr "nurbsCircle2_scaleX.o" "nurbsCircle2.sx";
connectAttr "nurbsCircle2_scaleY.o" "nurbsCircle2.sy";
connectAttr "nurbsCircle2_scaleZ.o" "nurbsCircle2.sz";
connectAttr "nurbsCircle2_visibility.o" "nurbsCircle2.v";
connectAttr "nurbsCircle2_blendParent1.o" "nurbsCircle2.blendParent1";
connectAttr "nurbsCircle2.ro" "nurbsCircle2_parentConstraint1.cro";
connectAttr "nurbsCircle2.pim" "nurbsCircle2_parentConstraint1.cpim";
connectAttr "nurbsCircle2.rp" "nurbsCircle2_parentConstraint1.crp";
connectAttr "nurbsCircle2.rpt" "nurbsCircle2_parentConstraint1.crt";
connectAttr "nurbsCircle1.t" "nurbsCircle2_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle1.rp" "nurbsCircle2_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle1.rpt" "nurbsCircle2_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle1.r" "nurbsCircle2_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle1.ro" "nurbsCircle2_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle1.s" "nurbsCircle2_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle1.pm" "nurbsCircle2_parentConstraint1.tg[0].tpm";
connectAttr "nurbsCircle2_parentConstraint1.w0" "nurbsCircle2_parentConstraint1.tg[0].tw"
		;
connectAttr "pairBlend1.otx" "nurbsCircle3.tx";
connectAttr "pairBlend1.oty" "nurbsCircle3.ty";
connectAttr "pairBlend1.otz" "nurbsCircle3.tz";
connectAttr "pairBlend1.orx" "nurbsCircle3.rx";
connectAttr "pairBlend1.ory" "nurbsCircle3.ry";
connectAttr "pairBlend1.orz" "nurbsCircle3.rz";
connectAttr "nurbsCircle3_scaleX.o" "nurbsCircle3.sx";
connectAttr "nurbsCircle3_scaleY.o" "nurbsCircle3.sy";
connectAttr "nurbsCircle3_scaleZ.o" "nurbsCircle3.sz";
connectAttr "nurbsCircle3_visibility.o" "nurbsCircle3.v";
connectAttr "nurbsCircle3_blendParent1.o" "nurbsCircle3.blendParent1";
connectAttr "nurbsCircle3.ro" "nurbsCircle3_parentConstraint1.cro";
connectAttr "nurbsCircle3.pim" "nurbsCircle3_parentConstraint1.cpim";
connectAttr "nurbsCircle3.rp" "nurbsCircle3_parentConstraint1.crp";
connectAttr "nurbsCircle3.rpt" "nurbsCircle3_parentConstraint1.crt";
connectAttr "nurbsCircle2.t" "nurbsCircle3_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle2.rp" "nurbsCircle3_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle2.rpt" "nurbsCircle3_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle2.r" "nurbsCircle3_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle2.ro" "nurbsCircle3_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle2.s" "nurbsCircle3_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle2.pm" "nurbsCircle3_parentConstraint1.tg[0].tpm";
connectAttr "nurbsCircle3_parentConstraint1.w0" "nurbsCircle3_parentConstraint1.tg[0].tw"
		;
connectAttr "pairBlend4.otx" "nurbsCircle4.tx";
connectAttr "pairBlend4.oty" "nurbsCircle4.ty";
connectAttr "pairBlend4.otz" "nurbsCircle4.tz";
connectAttr "pairBlend4.orx" "nurbsCircle4.rx";
connectAttr "pairBlend4.ory" "nurbsCircle4.ry";
connectAttr "pairBlend4.orz" "nurbsCircle4.rz";
connectAttr "nurbsCircle4_scaleX.o" "nurbsCircle4.sx";
connectAttr "nurbsCircle4_scaleY.o" "nurbsCircle4.sy";
connectAttr "nurbsCircle4_scaleZ.o" "nurbsCircle4.sz";
connectAttr "nurbsCircle4_visibility.o" "nurbsCircle4.v";
connectAttr "nurbsCircle4_blendParent1.o" "nurbsCircle4.blendParent1";
connectAttr "nurbsCircle4.ro" "nurbsCircle4_parentConstraint1.cro";
connectAttr "nurbsCircle4.pim" "nurbsCircle4_parentConstraint1.cpim";
connectAttr "nurbsCircle4.rp" "nurbsCircle4_parentConstraint1.crp";
connectAttr "nurbsCircle4.rpt" "nurbsCircle4_parentConstraint1.crt";
connectAttr "nurbsCircle7.t" "nurbsCircle4_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle7.rp" "nurbsCircle4_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle7.rpt" "nurbsCircle4_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle7.r" "nurbsCircle4_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle7.ro" "nurbsCircle4_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle7.s" "nurbsCircle4_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle7.pm" "nurbsCircle4_parentConstraint1.tg[0].tpm";
connectAttr "nurbsCircle4_parentConstraint1.w0" "nurbsCircle4_parentConstraint1.tg[0].tw"
		;
connectAttr "pairBlend5.otx" "nurbsCircle5.tx";
connectAttr "pairBlend5.oty" "nurbsCircle5.ty";
connectAttr "pairBlend5.otz" "nurbsCircle5.tz";
connectAttr "pairBlend5.orx" "nurbsCircle5.rx";
connectAttr "pairBlend5.ory" "nurbsCircle5.ry";
connectAttr "pairBlend5.orz" "nurbsCircle5.rz";
connectAttr "nurbsCircle5_scaleX.o" "nurbsCircle5.sx";
connectAttr "nurbsCircle5_scaleY.o" "nurbsCircle5.sy";
connectAttr "nurbsCircle5_scaleZ.o" "nurbsCircle5.sz";
connectAttr "nurbsCircle5_visibility.o" "nurbsCircle5.v";
connectAttr "nurbsCircle5_blendParent1.o" "nurbsCircle5.blendParent1";
connectAttr "nurbsCircle5.ro" "nurbsCircle5_parentConstraint1.cro";
connectAttr "nurbsCircle5.pim" "nurbsCircle5_parentConstraint1.cpim";
connectAttr "nurbsCircle5.rp" "nurbsCircle5_parentConstraint1.crp";
connectAttr "nurbsCircle5.rpt" "nurbsCircle5_parentConstraint1.crt";
connectAttr "nurbsCircle4.t" "nurbsCircle5_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle4.rp" "nurbsCircle5_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle4.rpt" "nurbsCircle5_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle4.r" "nurbsCircle5_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle4.ro" "nurbsCircle5_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle4.s" "nurbsCircle5_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle4.pm" "nurbsCircle5_parentConstraint1.tg[0].tpm";
connectAttr "nurbsCircle5_parentConstraint1.w0" "nurbsCircle5_parentConstraint1.tg[0].tw"
		;
connectAttr "pairBlend6.otx" "nurbsCircle6.tx";
connectAttr "pairBlend6.oty" "nurbsCircle6.ty";
connectAttr "pairBlend6.otz" "nurbsCircle6.tz";
connectAttr "pairBlend6.orx" "nurbsCircle6.rx";
connectAttr "pairBlend6.ory" "nurbsCircle6.ry";
connectAttr "pairBlend6.orz" "nurbsCircle6.rz";
connectAttr "nurbsCircle6_scaleX.o" "nurbsCircle6.sx";
connectAttr "nurbsCircle6_scaleY.o" "nurbsCircle6.sy";
connectAttr "nurbsCircle6_scaleZ.o" "nurbsCircle6.sz";
connectAttr "nurbsCircle6_visibility.o" "nurbsCircle6.v";
connectAttr "nurbsCircle6_blendParent1.o" "nurbsCircle6.blendParent1";
connectAttr "nurbsCircle6.ro" "nurbsCircle6_parentConstraint1.cro";
connectAttr "nurbsCircle6.pim" "nurbsCircle6_parentConstraint1.cpim";
connectAttr "nurbsCircle6.rp" "nurbsCircle6_parentConstraint1.crp";
connectAttr "nurbsCircle6.rpt" "nurbsCircle6_parentConstraint1.crt";
connectAttr "nurbsCircle5.t" "nurbsCircle6_parentConstraint1.tg[0].tt";
connectAttr "nurbsCircle5.rp" "nurbsCircle6_parentConstraint1.tg[0].trp";
connectAttr "nurbsCircle5.rpt" "nurbsCircle6_parentConstraint1.tg[0].trt";
connectAttr "nurbsCircle5.r" "nurbsCircle6_parentConstraint1.tg[0].tr";
connectAttr "nurbsCircle5.ro" "nurbsCircle6_parentConstraint1.tg[0].tro";
connectAttr "nurbsCircle5.s" "nurbsCircle6_parentConstraint1.tg[0].ts";
connectAttr "nurbsCircle5.pm" "nurbsCircle6_parentConstraint1.tg[0].tpm";
connectAttr "nurbsCircle6_parentConstraint1.w0" "nurbsCircle6_parentConstraint1.tg[0].tw"
		;
connectAttr "makeNurbCircle2.oc" "nurbsCircleShape7.cr";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "lambert2SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "lambert2SG.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "polySurfaceShape1.o" "polyMergeVert1.ip";
connectAttr "pCubeShape2.wm" "polyMergeVert1.mp";
connectAttr "polySurfaceShape2.o" "polyMergeVert2.ip";
connectAttr "pCubeShape1.wm" "polyMergeVert2.mp";
connectAttr "pCubeShape2.o" "polyUnite1.ip[0]";
connectAttr "pCubeShape1.o" "polyUnite1.ip[1]";
connectAttr "pCubeShape2.wm" "polyUnite1.im[0]";
connectAttr "pCubeShape1.wm" "polyUnite1.im[1]";
connectAttr "polyMergeVert1.out" "groupParts1.ig";
connectAttr "groupId1.id" "groupParts1.gi";
connectAttr "polyMergeVert2.out" "groupParts2.ig";
connectAttr "groupId3.id" "groupParts2.gi";
connectAttr "polyUnite1.out" "polyMergeVert3.ip";
connectAttr "pCube3Shape.wm" "polyMergeVert3.mp";
connectAttr "polyTweak1.out" "polyExtrudeFace1.ip";
connectAttr "pCube3Shape.wm" "polyExtrudeFace1.mp";
connectAttr "polyMergeVert3.out" "polyTweak1.ip";
connectAttr "polyTweak2.out" "polyExtrudeFace2.ip";
connectAttr "pCube3Shape.wm" "polyExtrudeFace2.mp";
connectAttr "polyExtrudeFace1.out" "polyTweak2.ip";
connectAttr "polyExtrudeFace2.out" "polyTweak3.ip";
connectAttr "polyTweak3.out" "polySplit1.ip";
connectAttr "polySplit1.out" "polySplit2.ip";
connectAttr "polySplit2.out" "polySplit3.ip";
connectAttr "polySplit3.out" "polySplit4.ip";
connectAttr "polySplit4.out" "polySplit5.ip";
connectAttr "polySplit5.out" "polySplit6.ip";
connectAttr "polySplit6.out" "polyTweak4.ip";
connectAttr "polyTweak4.out" "polySplit7.ip";
connectAttr "polyTweak5.out" "polySoftEdge1.ip";
connectAttr "pCube3Shape.wm" "polySoftEdge1.mp";
connectAttr "polySplit7.out" "polyTweak5.ip";
connectAttr "polySoftEdge1.out" "polyDelEdge1.ip";
connectAttr "polyDelEdge1.out" "polySplit8.ip";
connectAttr "polySplit8.out" "polySoftEdge2.ip";
connectAttr "pCube3Shape.wm" "polySoftEdge2.mp";
connectAttr "skinCluster1GroupParts.og" "skinCluster1.ip[0].ig";
connectAttr "skinCluster1GroupId.id" "skinCluster1.ip[0].gi";
connectAttr "bindPose1.msg" "skinCluster1.bp";
connectAttr "joint1.wm" "skinCluster1.ma[0]";
connectAttr "joint2.wm" "skinCluster1.ma[1]";
connectAttr "joint3.wm" "skinCluster1.ma[2]";
connectAttr "joint4.wm" "skinCluster1.ma[3]";
connectAttr "joint5.wm" "skinCluster1.ma[4]";
connectAttr "joint6.wm" "skinCluster1.ma[5]";
connectAttr "joint7.wm" "skinCluster1.ma[6]";
connectAttr "joint1.liw" "skinCluster1.lw[0]";
connectAttr "joint2.liw" "skinCluster1.lw[1]";
connectAttr "joint3.liw" "skinCluster1.lw[2]";
connectAttr "joint4.liw" "skinCluster1.lw[3]";
connectAttr "joint5.liw" "skinCluster1.lw[4]";
connectAttr "joint6.liw" "skinCluster1.lw[5]";
connectAttr "joint7.liw" "skinCluster1.lw[6]";
connectAttr "joint1.obcc" "skinCluster1.ifcl[0]";
connectAttr "joint2.obcc" "skinCluster1.ifcl[1]";
connectAttr "joint3.obcc" "skinCluster1.ifcl[2]";
connectAttr "joint4.obcc" "skinCluster1.ifcl[3]";
connectAttr "joint5.obcc" "skinCluster1.ifcl[4]";
connectAttr "joint6.obcc" "skinCluster1.ifcl[5]";
connectAttr "joint7.obcc" "skinCluster1.ifcl[6]";
connectAttr "groupParts5.og" "tweak1.ip[0].ig";
connectAttr "groupId7.id" "tweak1.ip[0].gi";
connectAttr "skinCluster1GroupId.msg" "skinCluster1Set.gn" -na;
connectAttr "pCube3Shape.iog.og[7]" "skinCluster1Set.dsm" -na;
connectAttr "skinCluster1.msg" "skinCluster1Set.ub[0]";
connectAttr "tweak1.og[0]" "skinCluster1GroupParts.ig";
connectAttr "skinCluster1GroupId.id" "skinCluster1GroupParts.gi";
connectAttr "groupId7.msg" "tweakSet1.gn" -na;
connectAttr "pCube3Shape.iog.og[8]" "tweakSet1.dsm" -na;
connectAttr "tweak1.msg" "tweakSet1.ub[0]";
connectAttr "polyPlanarProj1.out" "groupParts5.ig";
connectAttr "groupId7.id" "groupParts5.gi";
connectAttr "joint1.msg" "bindPose1.m[0]";
connectAttr "joint2.msg" "bindPose1.m[1]";
connectAttr "joint3.msg" "bindPose1.m[2]";
connectAttr "joint4.msg" "bindPose1.m[3]";
connectAttr "joint5.msg" "bindPose1.m[4]";
connectAttr "joint6.msg" "bindPose1.m[5]";
connectAttr "joint7.msg" "bindPose1.m[6]";
connectAttr "bindPose1.w" "bindPose1.p[0]";
connectAttr "bindPose1.m[0]" "bindPose1.p[1]";
connectAttr "bindPose1.m[1]" "bindPose1.p[2]";
connectAttr "bindPose1.m[2]" "bindPose1.p[3]";
connectAttr "bindPose1.m[0]" "bindPose1.p[4]";
connectAttr "bindPose1.m[4]" "bindPose1.p[5]";
connectAttr "bindPose1.m[5]" "bindPose1.p[6]";
connectAttr "joint1.bps" "bindPose1.wm[0]";
connectAttr "joint2.bps" "bindPose1.wm[1]";
connectAttr "joint3.bps" "bindPose1.wm[2]";
connectAttr "joint4.bps" "bindPose1.wm[3]";
connectAttr "joint5.bps" "bindPose1.wm[4]";
connectAttr "joint6.bps" "bindPose1.wm[5]";
connectAttr "joint7.bps" "bindPose1.wm[6]";
connectAttr "nurbsCircle3_parentConstraint1.ctx" "pairBlend1.itx2";
connectAttr "nurbsCircle3_parentConstraint1.cty" "pairBlend1.ity2";
connectAttr "nurbsCircle3_parentConstraint1.ctz" "pairBlend1.itz2";
connectAttr "nurbsCircle3_parentConstraint1.crx" "pairBlend1.irx2";
connectAttr "nurbsCircle3_parentConstraint1.cry" "pairBlend1.iry2";
connectAttr "nurbsCircle3_parentConstraint1.crz" "pairBlend1.irz2";
connectAttr "nurbsCircle3.blendParent1" "pairBlend1.w";
connectAttr "pairBlend1_inTranslateX1.o" "pairBlend1.itx1";
connectAttr "pairBlend1_inTranslateY1.o" "pairBlend1.ity1";
connectAttr "pairBlend1_inTranslateZ1.o" "pairBlend1.itz1";
connectAttr "pairBlend1_inRotateX1.o" "pairBlend1.irx1";
connectAttr "pairBlend1_inRotateY1.o" "pairBlend1.iry1";
connectAttr "pairBlend1_inRotateZ1.o" "pairBlend1.irz1";
connectAttr "nurbsCircle2_parentConstraint1.ctx" "pairBlend2.itx2";
connectAttr "nurbsCircle2_parentConstraint1.cty" "pairBlend2.ity2";
connectAttr "nurbsCircle2_parentConstraint1.ctz" "pairBlend2.itz2";
connectAttr "nurbsCircle2_parentConstraint1.crx" "pairBlend2.irx2";
connectAttr "nurbsCircle2_parentConstraint1.cry" "pairBlend2.iry2";
connectAttr "nurbsCircle2_parentConstraint1.crz" "pairBlend2.irz2";
connectAttr "nurbsCircle2.blendParent1" "pairBlend2.w";
connectAttr "pairBlend2_inTranslateX1.o" "pairBlend2.itx1";
connectAttr "pairBlend2_inTranslateY1.o" "pairBlend2.ity1";
connectAttr "pairBlend2_inTranslateZ1.o" "pairBlend2.itz1";
connectAttr "pairBlend2_inRotateX1.o" "pairBlend2.irx1";
connectAttr "pairBlend2_inRotateY1.o" "pairBlend2.iry1";
connectAttr "pairBlend2_inRotateZ1.o" "pairBlend2.irz1";
connectAttr "nurbsCircle1_parentConstraint1.ctx" "pairBlend3.itx2";
connectAttr "nurbsCircle1_parentConstraint1.cty" "pairBlend3.ity2";
connectAttr "nurbsCircle1_parentConstraint1.ctz" "pairBlend3.itz2";
connectAttr "nurbsCircle1_parentConstraint1.crx" "pairBlend3.irx2";
connectAttr "nurbsCircle1_parentConstraint1.cry" "pairBlend3.iry2";
connectAttr "nurbsCircle1_parentConstraint1.crz" "pairBlend3.irz2";
connectAttr "nurbsCircle1.blendParent1" "pairBlend3.w";
connectAttr "pairBlend3_inTranslateX1.o" "pairBlend3.itx1";
connectAttr "pairBlend3_inTranslateY1.o" "pairBlend3.ity1";
connectAttr "pairBlend3_inTranslateZ1.o" "pairBlend3.itz1";
connectAttr "pairBlend3_inRotateX1.o" "pairBlend3.irx1";
connectAttr "pairBlend3_inRotateY1.o" "pairBlend3.iry1";
connectAttr "pairBlend3_inRotateZ1.o" "pairBlend3.irz1";
connectAttr "nurbsCircle4_parentConstraint1.ctx" "pairBlend4.itx2";
connectAttr "nurbsCircle4_parentConstraint1.cty" "pairBlend4.ity2";
connectAttr "nurbsCircle4_parentConstraint1.ctz" "pairBlend4.itz2";
connectAttr "nurbsCircle4_parentConstraint1.crx" "pairBlend4.irx2";
connectAttr "nurbsCircle4_parentConstraint1.cry" "pairBlend4.iry2";
connectAttr "nurbsCircle4_parentConstraint1.crz" "pairBlend4.irz2";
connectAttr "nurbsCircle4.blendParent1" "pairBlend4.w";
connectAttr "pairBlend4_inTranslateX1.o" "pairBlend4.itx1";
connectAttr "pairBlend4_inTranslateY1.o" "pairBlend4.ity1";
connectAttr "pairBlend4_inTranslateZ1.o" "pairBlend4.itz1";
connectAttr "pairBlend4_inRotateX1.o" "pairBlend4.irx1";
connectAttr "pairBlend4_inRotateY1.o" "pairBlend4.iry1";
connectAttr "pairBlend4_inRotateZ1.o" "pairBlend4.irz1";
connectAttr "nurbsCircle5_parentConstraint1.ctx" "pairBlend5.itx2";
connectAttr "nurbsCircle5_parentConstraint1.cty" "pairBlend5.ity2";
connectAttr "nurbsCircle5_parentConstraint1.ctz" "pairBlend5.itz2";
connectAttr "nurbsCircle5_parentConstraint1.crx" "pairBlend5.irx2";
connectAttr "nurbsCircle5_parentConstraint1.cry" "pairBlend5.iry2";
connectAttr "nurbsCircle5_parentConstraint1.crz" "pairBlend5.irz2";
connectAttr "nurbsCircle5.blendParent1" "pairBlend5.w";
connectAttr "pairBlend5_inTranslateX1.o" "pairBlend5.itx1";
connectAttr "pairBlend5_inTranslateY1.o" "pairBlend5.ity1";
connectAttr "pairBlend5_inTranslateZ1.o" "pairBlend5.itz1";
connectAttr "pairBlend5_inRotateX1.o" "pairBlend5.irx1";
connectAttr "pairBlend5_inRotateY1.o" "pairBlend5.iry1";
connectAttr "pairBlend5_inRotateZ1.o" "pairBlend5.irz1";
connectAttr "nurbsCircle6_parentConstraint1.ctx" "pairBlend6.itx2";
connectAttr "nurbsCircle6_parentConstraint1.cty" "pairBlend6.ity2";
connectAttr "nurbsCircle6_parentConstraint1.ctz" "pairBlend6.itz2";
connectAttr "nurbsCircle6_parentConstraint1.crx" "pairBlend6.irx2";
connectAttr "nurbsCircle6_parentConstraint1.cry" "pairBlend6.iry2";
connectAttr "nurbsCircle6_parentConstraint1.crz" "pairBlend6.irz2";
connectAttr "nurbsCircle6.blendParent1" "pairBlend6.w";
connectAttr "pairBlend6_inTranslateX1.o" "pairBlend6.itx1";
connectAttr "pairBlend6_inTranslateY1.o" "pairBlend6.ity1";
connectAttr "pairBlend6_inTranslateZ1.o" "pairBlend6.itz1";
connectAttr "pairBlend6_inRotateX1.o" "pairBlend6.irx1";
connectAttr "pairBlend6_inRotateY1.o" "pairBlend6.iry1";
connectAttr "pairBlend6_inRotateZ1.o" "pairBlend6.irz1";
connectAttr "pCube3ShapeOrig.w" "polyPlanarProj1.ip";
connectAttr "pCube3Shape.wm" "polyPlanarProj1.mp";
connectAttr "skinCluster1.og[0]" "polyMapCut1.ip";
connectAttr "polyMapCut1.out" "polyTweakUV1.ip";
connectAttr "file1.oc" "lambert2.c";
connectAttr "lambert2.oc" "lambert2SG.ss";
connectAttr "pCube3Shape.iog" "lambert2SG.dsm" -na;
connectAttr "lambert2SG.msg" "materialInfo1.sg";
connectAttr "lambert2.msg" "materialInfo1.m";
connectAttr "file1.msg" "materialInfo1.t" -na;
connectAttr ":defaultColorMgtGlobals.cme" "file1.cme";
connectAttr ":defaultColorMgtGlobals.cfe" "file1.cmcf";
connectAttr ":defaultColorMgtGlobals.cfp" "file1.cmcp";
connectAttr ":defaultColorMgtGlobals.wsn" "file1.ws";
connectAttr "place2dTexture1.c" "file1.c";
connectAttr "place2dTexture1.tf" "file1.tf";
connectAttr "place2dTexture1.rf" "file1.rf";
connectAttr "place2dTexture1.mu" "file1.mu";
connectAttr "place2dTexture1.mv" "file1.mv";
connectAttr "place2dTexture1.s" "file1.s";
connectAttr "place2dTexture1.wu" "file1.wu";
connectAttr "place2dTexture1.wv" "file1.wv";
connectAttr "place2dTexture1.re" "file1.re";
connectAttr "place2dTexture1.of" "file1.of";
connectAttr "place2dTexture1.r" "file1.ro";
connectAttr "place2dTexture1.n" "file1.n";
connectAttr "place2dTexture1.vt1" "file1.vt1";
connectAttr "place2dTexture1.vt2" "file1.vt2";
connectAttr "place2dTexture1.vt3" "file1.vt3";
connectAttr "place2dTexture1.vc1" "file1.vc1";
connectAttr "place2dTexture1.o" "file1.uv";
connectAttr "place2dTexture1.ofs" "file1.fs";
connectAttr "lambert2SG.msg" "hyperShadePrimaryNodeEditorSavedTabsInfo.tgi[0].ni[0].dn"
		;
connectAttr "lambert2.msg" "hyperShadePrimaryNodeEditorSavedTabsInfo.tgi[0].ni[1].dn"
		;
connectAttr "place2dTexture1.msg" "hyperShadePrimaryNodeEditorSavedTabsInfo.tgi[0].ni[2].dn"
		;
connectAttr "file1.msg" "hyperShadePrimaryNodeEditorSavedTabsInfo.tgi[0].ni[3].dn"
		;
connectAttr "lambert2SG.pa" ":renderPartition.st" -na;
connectAttr "lambert2.msg" ":defaultShaderList1.s" -na;
connectAttr "place2dTexture1.msg" ":defaultRenderUtilityList1.u" -na;
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
connectAttr "file1.msg" ":defaultTextureList1.tx" -na;
connectAttr "pCubeShape2.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape2.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape1.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape1.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "groupId1.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId2.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId3.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId4.msg" ":initialShadingGroup.gn" -na;
// End of Mouette.ma
