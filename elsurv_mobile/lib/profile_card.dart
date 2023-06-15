import 'package:flutter/material.dart';

import 'circle_image.dart';
import 'environment.dart';
import 'main_theme.dart';

class AuthorCard extends StatelessWidget {
  final String guestName;
  final String title;
  final ImageProvider? imageProvider;

  const AuthorCard({
    super.key,
    required this.guestName,
    required this.title,
    this.imageProvider,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.all(16),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Row(children: [
            CircleImage(
              imageProvider: imageProvider,
              imageRadius: 28,
            ),
            const SizedBox(width: 8),
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  guestName,
                  style: ElSurvTheme.lightTextTheme.displayMedium,
                ),
                Text(
                  title,
                  style: ElSurvTheme.lightTextTheme.displaySmall,
                )
              ],
            ),
          ]),
          /*
          Text('Bonuce: ${Params.bonuce.toString()}'),
          
          IconButton(
            icon: const Icon(Icons.favorite_border),
            iconSize: 30,
            color: Colors.grey[400],
            onPressed: () {
              const snackBar = SnackBar(content: Text('Favorite Pressed'));
              ScaffoldMessenger.of(context).showSnackBar(snackBar);
            },
          ),
          */
        ],
      ),
    );
  }
}
