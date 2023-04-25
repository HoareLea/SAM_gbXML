[![Build Status](https://hldigitalinnovation.visualstudio.com/HLApps/_apis/build/status/SAM-deploy-SAM_gbXML?branchName=master)](https://hldigitalinnovation.visualstudio.com/HLApps/_build/latest?definitionId=20&branchName=master)

# SAM_gbXML

<a href="https://github.com/HoareLea/SAM_gbXML"><img src="https://github.com/HoareLea/SAM_gbXML/blob/master/Grasshopper/SAM.Analytical.Grasshopper.gbXML/Resources/SAM_gbXML.png" align="left" hspace="10" vspace="6"></a>

**SAM_gbXML** is part of SAM Toolkit that is designed to help export/import gbXML from SAM Analytical Model. Welcome and let's make the opensource journey continue. :handshake:
SAM_gbXML is a C# library that provides functionality for working with gbXML files, a standard format used to exchange building information data.

## Features

- Convert gbXML geometry objects to SAM geometry objects
- Analyze gbXML geometry objects and calculate their properties
- Create gbXML geometry objects from scratch
- Validate gbXML files

## Resources
* [Wiki](https://github.com/HoareLea/SAM_gbXML/wiki)

## Installing

To install **SAM** from .exe just download and run [latest installer](https://github.com/HoareLea/SAM_Deploy/releases) otherwise rebuild using VS [SAM](https://github.com/HoareLea/SAM) and [SAM_gbXML](https://github.com/HoareLea/SAM_gbXML)

## Usage

Converting gbXML to SAM
The library provides extension methods that allow you to easily convert gbXML geometry objects to SAM geometry objects. For example, to convert a gbXML opening object to a SAM aperture object, you can use the following code:

```csharp
using SAM.Analytical.gbXML;

...

var gbxmlOpening = ...; // get the gbXML opening object
var samAperture = gbxmlOpening.ToSAM();
```
Analyzing gbXML geometry objects
The library also provides methods for analyzing gbXML geometry objects and calculating their properties. For example, to calculate the area of a gbXML polygon object, you can use the following code:

```csharp
using SAM.Geometry.gbXML;

...

var gbxmlPolygon = ...; // get the gbXML polygon object
var area = gbxmlPolygon.GetArea();
```

Creating gbXML geometry objects
You can also create gbXML geometry objects from scratch using the classes provided in the library. For example, to create a gbXML polygon object, you can use the following code:

```csharp
using SAM.Geometry.gbXML;

...

var vertices = new List<Point3D> { new Point3D(0, 0, 0), new Point3D(1, 0, 0), new Point3D(1, 1, 0), new Point3D(0, 1, 0) };
var gbxmlPolygon = new Polygon(vertices);
```

Validating gbXML files
The library provides methods for validating gbXML files. For example, to validate a gbXML file, you can use the following code:
```csharp
using SAM.Geometry.gbXML;

...

var gbxmlFile = ...; // get the path to the gbXML file
var errors = gbxmlFile.Validate();
```


## Contributing
If you would like to contribute to SAM_SolarCalculator, please fork the repository and submit a pull request. Before submitting a pull request, please make sure that your code adheres to the coding standards and conventions used in the project.


## Licence ##

SAM is free software licenced under GNU Lesser General Public Licence - [https://www.gnu.org/licenses/lgpl-3.0.html](https://www.gnu.org/licenses/lgpl-3.0.html)  
Each contributor holds copyright over their respective contributions.
The project versioning (Git) records all such contribution source information.
See [LICENSE](https://github.com/HoareLea/SAM_gbXML/blob/master/LICENSE) and [COPYRIGHT_HEADER](https://github.com/HoareLea/SAM/blob/master/COPYRIGHT_HEADER.txt).