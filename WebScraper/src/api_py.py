#!/usr/bin/env python
import pika, sys, os
import json
import run
from pika.spec import BasicProperties
from wikimodels import wordBankQueue
def main():
    connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost'))
    channel = connection.channel()

    channel.queue_declare(queue='wiki_surf_dev')

    def callback(ch, method, properties, body):
        test = body.decode()
        test = json.loads(test)
        sessionId = test.get("WikiSessionId")
        print(" [x] Received WordBankId: %s WordBankQueueId: %s" % (test.get("WordBankId"), test.get("WordBankQueueId")))        
        process = run.WikiProcess()
        res = process.run(test, sessionId)                    
        tt = json.dumps(res, indent = 4, sort_keys=True, default = str).encode('utf-8')
        p = pika.BasicProperties(correlation_id=properties.correlation_id)
        channel.basic_publish(exchange = "", routing_key = properties.reply_to,properties = p ,body = tt)
        
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