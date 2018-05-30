import datetime
import sys
#print(datetime.datetime.now())
import threading
from PIL import Image
import requests
from io import BytesIO
import cv2

source = "http://192.168.1.101:8088/stream?topic=/camera/color/image_raw"
target = "D:\Projects\FinalProject\CSA_Project\CSA_Project\Euclid"

class EuclidCamera(object):
    def __init__(self):
        # Using OpenCV to capture from device 0. If you have trouble capturing
        # from a webcam, comment the line below out and use a video file
        # instead.
        self.video = cv2.VideoCapture(source)
        # If you decide to use video.mp4, you must have this file in the folder
        # as the main.py.
        # self.video = cv2.VideoCapture('video.mp4')

    def __del__(self):
        self.video.release()

    def get_frame(self):
        success, image = self.video.read()
        # We are using Motion JPEG, but OpenCV defaults to capture raw images,
        # so we must encode it into JPEG in order to correctly display the
        # video stream.
        ret, jpeg = cv2.imencode('.jpg', image)
        return jpeg.tobytes()
