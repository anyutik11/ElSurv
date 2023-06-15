import 'dart:convert';
import 'dart:developer';
import 'package:http/http.dart' as http;
import '../environment.dart';
import '../model/bonuce_model.dart';
import '../model/company_model.dart';
import '../model/guest_model.dart';
import '../model/review_model.dart';

Future<List<Company>> fetchCompanies() async {
  final response = await http.get(
      Uri.parse('${Params.host}MobileApi/CompanyList?key=${Params.apiKey}'));

  if (response.statusCode == 200) {
    final Iterable l = json.decode(response.body);
    final posts = List<Company>.from(l.map((model) => Company.fromJson(model)));

    log('REQ Companies: ${posts[0].shortName}');

    return posts;
  } else {
    throw Exception('Failed to load companies');
  }
}

Future<List<Review>> fetchReviews(String companyId) async {
  final response = await http.get(Uri.parse(
      '${Params.host}MobileApi/ReviewList/${companyId}?key=${Params.apiKey}'));

  if (response.statusCode == 200) {
    final Iterable l = json.decode(response.body);
    final posts = List<Review>.from(l.map((model) => Review.fromJson(model)));

    log('REQ Reviews: ${posts.length}');

    return posts;
  } else {
    throw Exception('Failed to load reviews');
  }
}

Future postResult() async {
  final response = await http.post(
      Uri.parse(
          '${Params.host}MobileApi/postResult/${Params.reviewId}?key=${Params.apiKey}'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(Params.answers));

  if (response.statusCode == 200) {
    log('REQ postResult OK');
  } else {
    throw Exception('Failed to post results');
  }
}

Future<List<Bonuce>> fetchBonuces(String guestId) async {
  final response = await http.get(Uri.parse(
      '${Params.host}MobileApi/BonuceList/${guestId}?key=${Params.apiKey}'));

  if (response.statusCode == 200) {
    final Iterable l = json.decode(response.body);
    final posts = List<Bonuce>.from(l.map((model) => Bonuce.fromJson(model)));
    posts.sort((a, b) => b.dtd.compareTo(a.dtd));

    log('REQ Bonuce: ${posts.length}');

    return posts;
  } else {
    throw Exception('Failed to load bonuces');
  }
}

Future<Guest> fetchLogin(String login, String pass) async {
  final response = await http.post(
      Uri.parse('${Params.host}MobileApi/login?key=${Params.apiKey}'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode([login, pass]));

  if (response.statusCode == 200) {
    log('REQ fetchLogin OK');
    final resp = json.decode(response.body);
    return Guest.fromJson(resp);
  } else {
    log('Failed to login');
    const guest = Guest(
        id: '',
        gkey: '',
        surname: '',
        name: '',
        email: '',
        phone: '',
        dateBirth: '',
        gender: '');
    return guest;
  }
}
