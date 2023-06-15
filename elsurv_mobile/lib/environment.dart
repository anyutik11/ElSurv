import 'model/guest_model.dart';

class Params {
  // Для web-версии
  //static String host = 'https://localhost:44324/';
  // Для android-emulator
  //static String host = 'http://10.0.2.2:44324/';
  static String host = 'https://customerpulse.ru/';
  static String apiKey = 'jdh4pk901n3bna5';

  static List<String> answers = ['', '', ''];
  static String reviewId = '';
  static Guest? guest;
}
