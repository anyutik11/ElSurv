import 'dart:io';

import 'package:flutter/material.dart';

import 'environment.dart';
import 'main_theme.dart';
import 'router/custom_router.dart';
import 'router/route_paths.dart';

void main() {
  if (Params.host.contains('10.0.2.2')) {
    HttpOverrides.global = MyHttpOverrides();
  }

  runApp(const ElSurv());
}

class ElSurv extends StatelessWidget {
  const ElSurv({super.key});

  @override
  Widget build(BuildContext context) {
    final theme = ElSurvTheme.dark();
    return MaterialApp(
      theme: theme,
      title: 'ElSurv',
      debugShowCheckedModeBanner: false,
      onGenerateRoute: CustomRouter.onGenerateRoute,
      initialRoute: RoutePaths.Splash,
    );
  }
}

class MyHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext? context) {
    return super.createHttpClient(context)
      ..badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
  }
}
