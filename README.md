# RI_OperatorCommands

**RI_OperatorCommands** is a .NET library designed to simplify the integration and interaction with industrial robots, particularly those using the FANUC R-30iB or similar controllers. This library provides a set of functions and classes that make it easy to connect to, control, and monitor these robots from your .NET applications.

## Key Features
- **Connection Management**: Establish and manage connections to FANUC industrial robots using IP addresses.
- **Read/Write Robot Data**: Read and write various robot data, including position registers, system variables, and more.
- **Control Robot Movements**: Control the robot's movement by setting joint positions, cartesian positions, and more.
- **I/O Operations**: Perform input and output operations with digital and analog signals.

## Usage
Here's a quick overview of how to use the library's key functions:

1. **Connecting to a Robot**:
   ```csharp
   Commands commands = new Commands();
   bool isConnected = commands.ConnectTo("RobotIPAddress", DataPosRegStartIndex, DataPosRegEndIndex);
   ```

2. **Reading Robot Data**:  
   ```csharp
   Array xyzwpr = new float[6];
   bool success = commands.ReadActualPos(ref xyzwpr);

 3.**Writing Robot Data**:
   ```csharp
    float[] cartesianValues = new float[] { X, Y, Z, W, P, R };
    short UF = 1; // User frame
    short UT = 1; // User tool
    bool success = commands.WriteCartPOS(PR, cartesianValues, UF, UT);
```

4.**Disconnecting from the Robot:**
   ```csharp

commands.DisconnectRobot();
```



**License**
This library is open-source and released under the MIT License. You are free to use, modify, and distribute it according to the terms of the license.

**Contributing**
Contributions, bug reports, and feature requests are welcome. Feel free to open an issue or create a pull request on GitHub.



