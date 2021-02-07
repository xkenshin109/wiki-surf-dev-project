#!/usr/bin/env python
import pika, sys, os
import json
import run
from pika.spec import BasicProperties
def main():
    connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost'))
    channel = connection.channel()

    channel.queue_declare(queue='wiki_surf_dev')

    def callback(ch, method, properties, body):
        test = body.decode()
        test = json.loads(test)
        print(" [x] Received WordBankId: %s WordBankQueueId: %s" % (test.get("WordBankId"), test.get("WordBankQueueId")))        
        run.WikiProcess().run(test)
        p = pika.BasicProperties(correlation_id=properties.correlation_id)
        channel.basic_publish(exchange = "", routing_key = properties.reply_to,properties = p ,body = str("received"))
    channel.basic_consume(queue='wiki_surf_dev', on_message_callback=callback, auto_ack=True)

    print(' [*] Waiting for messages. To exit press CTRL+C')
    channel.start_consuming()

if __name__ == '__main__':
    try:
        main()
    except KeyboardInterrupt:
        print('Interrupted')
        try:
            sys.exit(0)
        except SystemExit:
            os._exit(0)