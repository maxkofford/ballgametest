using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class moveball : MonoBehaviour {

    public static void testPython()
    {
        Debug.Log("*********************************TEEEEEEEEEEEEEEEEEEEEEEEESSSSSSSSSSSSTTTTTTTTTTTTTTTTT");
        
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
        process.StartInfo = startInfo;
        process.Start();
        string output = p.StandardOutput.ReadToEnd();
 p.WaitForExit();

        Debug.Log(output);
 /*

        // Start the child process.
 Process p = new Process();
 // Redirect the output stream of the child process.
 p.StartInfo.UseShellExecute = false;
 p.StartInfo.RedirectStandardOutput = true;
 p.StartInfo.FileName = "YOURBATCHFILE.bat";
 p.Start();
 // Do not wait for the child process to exit before
 // reading to the end of its redirected stream.
 // p.WaitForExit();
 // Read the output stream first and then wait.
 string output = p.StandardOutput.ReadToEnd();
 p.WaitForExit();

        */
    }


    public float speed;
   // public Text countText;
   // public Text winText;

    private Rigidbody rb;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
       // winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
       // countText.text = "Count: " + count.ToString();
        if (count >= 7)
        {
           // winText.text = "You Win!";
        }
    }
}
