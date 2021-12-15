# C4TAssessment
This repo contains the C4T assessment given to Tushar Kukreti

1 Introduction

Aassessment consists of two parts described below. 

2 Technical assessment

  2.1 Prerequisites
  
    - IDE for programming .NET code installed on your computer
    
    - .NET core SDK installed on computer
    
  2.2 Requirements
  
    - Source code needs to be made available on GitHub
    
    - HTTP POST endpoint: /api/enquiries
    
      o Body: JSON object
      
    ▪ Name string
    
    - Use https://restcountries.eu/ to search all countries that contain the provided name string
    
    - For each country, put following content on an Azure Service Bus queue. The name of the queue needs to be configurable via application settings.
    
      o Body
      
        ▪ Name name of the country (Dutch name)
        ▪ Code alpha2 code of the country
        ▪ RegionalBlocs array of RegionalBlocs (see below)
        ▪ BrowserName name of the browser that was used to make the req
        ▪ Timestamp the moment the request was made
      o RegionalBloc
        ▪ Name name of the regionalBloc
        ▪ Code acronym of the regionalBloc
        ▪ Countries array of all countries (their Dutch name) that are part of this regionalBloc
    - Make sure your solution is tested
    - Use .NET core
3 Presentation assessment

    - Create a Powerpoint presentation in which you
      o describe choices you needed to make and why you made your particular choices
      o visualize the message flow between the several components using an UML sequence diagram
      o explain the SOLID principles
    - Include the Powerpoint in your GitHub repo
