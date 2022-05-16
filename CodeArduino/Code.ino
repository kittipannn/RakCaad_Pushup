#define echoPin D2 // Echo Pin
#define trigPin D1 // Trigger Pin
#define LEDPin D4 // Onboard LED
#define alarm D3 // Onboard LED

int maximumRange = 200; // Maximum range needed
int minimumRange = 5; // Minimum range needed
long duration, distance; // Duration used to calculate distance

void setup() {
  Serial.begin (9600);
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  pinMode(LEDPin, OUTPUT); // Use LED indicator (if required)
  pinMode(alarm,OUTPUT);
}

void loop() {
  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);
  duration = pulseIn(echoPin, HIGH);

  //Calculate the distance (in cm) based on the speed of sound.
  distance = duration / 58.2;
  if (distance <= minimumRange ) {
    Serial.println("-1");  //เมื่ออยู่นอกระยะให้ใช้ Print -1
    digitalWrite(LEDPin, HIGH);
    digitalWrite(alarm,0); //ส่งเสียงเมื่อไกลเกินระยะ
  }
  else {
    Serial.println(distance);  //แสดงค่าระยะทาง
    digitalWrite(LEDPin, LOW);
    digitalWrite(alarm,1); //ส่งเสียงเมื่อไกลเกินระยะ
  }
  delay(100);
}
