[![Build (Windows)](https://github.com/SAM-BIM/SAM_gbXML/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/SAM-BIM/SAM_gbXML/actions/workflows/build.yml)
[![Installer (latest)](https://img.shields.io/github/v/release/SAM-BIM/SAM_Deploy?label=installer)](https://github.com/SAM-BIM/SAM_Deploy/releases/latest)

# SAM_gbXML

<a href="https://github.com/SAM-BIM/SAM">
  <img src="https://github.com/SAM-BIM/SAM_gbXML/blob/master/Grasshopper/SAM.Analytical.Grasshopper.gbXML/Resources/SAM_gbXML.png"
       align="left" hspace="10" vspace="6">
</a>

**SAM_gbXML** is part of the **SAM (Sustainable Analytical Model) Toolkit** —  
an open-source collection of tools designed to help engineers create, manage,
and process analytical building models for energy and environmental analysis.

This repository provides **gbXML import and export utilities**
to enable interoperability between **SAM analytical models** and tools that use **gbXML**
as a building information exchange format.

It supports translation between gbXML geometry and SAM analytical representations,
with helper utilities for validation and geometry inspection.

Welcome — and let’s keep the open-source journey going. 🤝

---

## Features

- Import gbXML and translate gbXML geometry into SAM analytical objects
- Export SAM analytical models to gbXML
- Create and manipulate gbXML geometry programmatically
- Validate gbXML files and basic schema/geometry consistency

---

## Resources
- 📘 **SAM Wiki:** https://github.com/SAM-BIM/SAM/wiki  
- 🧠 **SAM Core:** https://github.com/SAM-BIM/SAM  
- 🧰 **Installers:** https://github.com/SAM-BIM/SAM_Deploy  

---

## Installing

To install **SAM** using the Windows installer, download and run the  
[latest installer](https://github.com/SAM-BIM/SAM_Deploy/releases/latest).

Alternatively, you can build the toolkit from source using Visual Studio.  
See the main repository for details:  
👉 https://github.com/SAM-BIM/SAM

---

## Development notes

- Target framework: **.NET / C#**
- gbXML mapping follows SAM-BIM analytical modelling conventions
- New or modified `.cs` files must include the SPDX header from `COPYRIGHT_HEADER.txt`

---

## Licence

This repository is free software licensed under the  
**GNU Lesser General Public License v3.0 or later (LGPL-3.0-or-later)**.

Each contributor retains copyright to their respective contributions.  
The project history (Git) records authorship and provenance of all changes.

See:
- `LICENSE`
- `NOTICE`
- `COPYRIGHT_HEADER.txt`
