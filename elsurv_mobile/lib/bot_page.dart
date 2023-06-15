import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:webview_flutter/webview_flutter.dart';

import 'environment.dart';

class BotPage extends StatelessWidget {
  const BotPage({super.key});

  @override
  Widget build(BuildContext context) {
    log('BotPage build');

    final wvController = WebViewController()
      ..setJavaScriptMode(JavaScriptMode.unrestricted)
      ..loadRequest(
        Uri.parse('${Params.host}MobileApi/Bot'),
      );

    return WebViewWidget(controller: wvController);

    /*
    return Scaffold(
      appBar: AppBar(
        title: const Text('Flutter WebView'),
      ),
      body: WebViewWidget(
        controller: wvController,
      ),
    );
    */
  }
}


  /*
  @override
  Widget build(BuildContext context) {
    return WebViewWidget(controller: controller);
  }
  */

  

