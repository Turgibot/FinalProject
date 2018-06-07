//g++ example.cpp -o app `pkg-config --cflags --libs opencv`

#include <opencv2/dnn.hpp>
#include <opencv2/dnn/shape_utils.hpp>
#include <opencv2/imgproc.hpp>
#include <opencv2/highgui.hpp>
using namespace cv;
using namespace cv::dnn;

#include <fstream>
#include <iostream>
#include <cstdlib>
using namespace std;

#define SIZE 300

const size_t width = SIZE;
const size_t height = SIZE;

static Mat getMean(const size_t& imageHeight, const size_t& imageWidth)
{
    Mat mean;

    const int meanValues[3] = {104, 117, 123};
    vector<Mat> meanChannels;
    for(int i = 0; i < 3; i++)
    {
        Mat channel((int)imageHeight, (int)imageWidth, CV_32F, Scalar(meanValues[i]));
        meanChannels.push_back(channel);
    }
    cv::merge(meanChannels, mean);
    return mean;
}

static Mat preprocess(const Mat& frame)
{
    Mat preprocessed;
    frame.convertTo(preprocessed, CV_32F);
    resize(preprocessed, preprocessed, Size(width, height)); //SSD accepts 300x300 RGB-images

    Mat mean = getMean(width, height);
    cv::subtract(preprocessed, mean, preprocessed);

    return preprocessed;
}

const char* about = "This sample uses Single-Shot Detector "
                    "(https://arxiv.org/abs/1512.02325)"
                    "to detect objects on image\n"; // TODO: link

const char* params
    = "{ help           | false | print usage         }"
      "{ proto          |       | model configuration }"
      "{ model          |       | model weights       }"
      "{ image          |       | image for detection }"
      "{ min_confidence | 0.5   | min confidence      }";

int main(int argc, char** argv)
{
    int numOfPerson =0;

    cv::CommandLineParser parser(argc, argv, params);

    if (parser.get<bool>("help"))
    {
        std::cout << about << std::endl;
        parser.printMessage();
        return 0;
    }

    String modelConfiguration = "/home/com/workspace/OpenCV_3.3/opencv/samples/cpp/example_cmake/MobileNetSSD_deploy.prototxt"; //parser.get<string>("proto");
    String modelBinary =  "/home/com/workspace/OpenCV_3.3/opencv/samples/cpp/example_cmake/MobileNetSSD_deploy.caffemodel";     //parser.get<string>("model");




    //! [Create the importer of Caffe model]
    Ptr<dnn::Importer> importer;

    // Import Caffe SSD model
    try
    {
        importer = dnn::createCaffeImporter(modelConfiguration, modelBinary);
    }
    catch (const cv::Exception &err) //Importer can throw errors, we will catch them
    {
        cerr << err.msg << endl;
    }
    //! [Create the importer of Caffe model]

    if (!importer)
    {
        cerr << "Can't load network by using the following files: " << endl;
        cerr << "prototxt:   " << modelConfiguration << endl;
        cerr << "caffemodel: " << modelBinary << endl;
        cerr << "Models can be downloaded here:" << endl;
        cerr << "https://github.com/weiliu89/caffe/tree/ssd#models" << endl;
        exit(-1);
    }

    //! [Initialize network]
    dnn::Net net;
    importer->populateNet(net);
    importer.release();          //We don't need importer anymore
    //! [Initialize network]
    VideoCapture cap(0);
    Mat frame;
    int frameount =0;
    //VideoWriter out;
    //out.open("/home/yakir/opencv-3.3.0/samples/cpp/example_cmake/out.avi", CV_FOURCC('M', 'J', 'P', 'G'), 15, Size(640,480), true);

    for(;;)
    {
        //cout<<" frameount "<<frameount<<endl;
        //cv::Mat frame = cv::imread("/home/yakir/opencv-3.3.0/samples/cpp/example_cmake/example_01.jpg", -1);
        cap >> frame;
        if( frameount % 1 == 0)
        {

           // cout<<"frame.size()"<<frame.size()<<endl;
            if (frame.channels() == 4)
                cvtColor(frame, frame, COLOR_BGRA2BGR);
            //! [Prepare blob]
            Mat preprocessedFrame = frame;//preprocess(frame);

            Mat inputBlob = blobFromImage(preprocessedFrame, 0.007843, Size(SIZE, SIZE), 127.5); //Convert Mat to batch of images
            //! [Prepare blob]

            //! [Set input blob]
            net.setInput(inputBlob, "data");                //set the network input
            //! [Set input blob]


            cv::TickMeter tm;
            tm.start();
            //! [Make forward pass]
            Mat detection = net.forward("detection_out");                                  //compute output
            //! [Make forward pass]

             tm.stop();

            // std::cout << "Inference time, ms: " << tm.getTimeMilli()  << std::endl;


            Mat detectionMat(detection.size[2], detection.size[3], CV_32F, detection.ptr<float>());

           // cout<<"detectionMat size"<<detectionMat.size()<<endl;


            float   confidenceThreshold = parser.get<float>("min_confidence");
            //cout<<"confidenceThreshold"<<confidenceThreshold<<endl;

            for(int i = 0; i < detectionMat.rows; i++)
            {
                float confidence =  detectionMat.at<float>(i, 2);
                // cout<<"confidence "<<confidence<<endl;



                if(confidence > confidenceThreshold)
                {
                    //cout<<"yakir3"<<endl;

                    size_t objectClass = (size_t)(detectionMat.at<float>(i, 1));

                    float xLeftBottom = detectionMat.at<float>(i, 3) * frame.cols;
                    float yLeftBottom = detectionMat.at<float>(i, 4) * frame.rows;
                    float xRightTop = detectionMat.at<float>(i, 5) * frame.cols;
                    float yRightTop = detectionMat.at<float>(i, 6) * frame.rows;

                    /*std::cout << "Class: " << objectClass << std::endl;
                    std::cout << "Confidence: " << confidence << std::endl;

                    std::cout << " " << xLeftBottom
                              << " " << yLeftBottom
                              << " " << xRightTop
                              << " " << yRightTop << std::endl;*/

                    Rect object((int)xLeftBottom, (int)yLeftBottom,
                                (int)(xRightTop - xLeftBottom),
                                (int)(yRightTop - yLeftBottom));

                    if( objectClass ==15)
                    {
                        //putText(frame,"PERSON",Point(object.x, object.y-10),2, 1.2f,Scalar(200,0,0),2);
                        rectangle(frame, object, Scalar(0, 255, 0),4);
                        numOfPerson++;
                    }
                }
            }
           // cout<<" numOfPerson "<<numOfPerson<<endl;


        }

        numOfPerson =0;
        imshow("detections", frame);
        waitKey(1);
        //out << frame;
        frameount++;




    }
    //out.release();


    return 0;
} // main


















// #include "opencv2/core.hpp"
// #include "opencv2/imgproc.hpp"
// #include "opencv2/highgui.hpp"
// #include "opencv2/videoio.hpp"
// #include <iostream>

// using namespace cv;
// using namespace std;

// void drawText(Mat & image);

// int main()
// {
//     cout << "Built with OpenCV " << CV_VERSION << endl;
//     Mat image;
//     VideoCapture capture;
//     capture.open(0);
//     if(capture.isOpened())
//     {
//         cout << "Capture is opened" << endl;
//         for(;;)
//         {
//             capture >> image;
//             if(image.empty())
//                 break;
//             drawText(image);
//             imshow("Sample", image);
//             if(waitKey(10) >= 0)
//                 break;
//         }
//     }
//     else
//     {
//         cout << "No capture" << endl;
//         image = Mat::zeros(480, 640, CV_8UC1);
//         drawText(image);
//         imshow("Sample", image);
//         waitKey(0);
//     }
//     return 0;
// }

// void drawText(Mat & image)
// {
//     putText(image, "Hello OpenCV",
//             Point(20, 50),
//             FONT_HERSHEY_COMPLEX, 1, // font face and scale
//             Scalar(255, 255, 255), // white
//             1, LINE_AA); // line thickness and type
// }
