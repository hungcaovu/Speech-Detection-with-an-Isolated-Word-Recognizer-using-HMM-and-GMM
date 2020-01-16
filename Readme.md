

Project: SPEECH DETECTION WITH AN ISOLATED WORD RECOGNIZER USING HMM AND GMM
          Recognize word from microphone.

Languages: 
    
    - C/C++ for DSP core processing (read audio files, mfcc processing, hmm, gmm, ...)
    - C# for the GUI
    - CRL porting C/C++ libraries to DLL for C#.

Structure: 

    1. Binary:
       Output after building the code for both mode release and debug
    2. Library: tinyXml lib for caching mfcc and model to file (i saved models, mfcc features to xml file.)
    3. Voice Comparison (the name ofproject doest not make sense, since from the beggining my   thesis project was comparison, after discussing with instructors, i change the goal of project to Speed detection.  )
        - Core folder: MFCC Extraction, HMM and GMM model (train and evaluate), Viterbi, ...
        * inc: *.h files
        * MSVC: Microsoft project files
        * src: *.cpp files
        - CSharp folder: GUI for trainning, recorder audio to train, and recognize word from microphone, design the app as MVC models
            * DLL: external libs, DevExpress for GUI, log4net for logging, NAudio for takking sound from the mic and save to wav file.
            * Model: Objects for GUI
            * View: Application GUI
        - Porting: Build and wrapper the DLL from c/c++ libs, so that the C# application could call C/C++ object through CRL DLL.
        

Building:
    You should use visual studio 2015 for this project.
    Install DevExpress v15 (i already attached the dll files)

    Open the project: Voice Comparison/Voice Comparison.sln

Contact:
    Email: hung.caovu@gmail.com, Skype: jimmy.hung.cao

******************************************************************************************
                        This is my master thesis project.

    
