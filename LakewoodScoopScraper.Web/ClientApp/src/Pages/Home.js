import React, { useState, useEffect } from 'react';
import axios from 'axios';


const Home = () => {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        const getPosts = async () => {
            const { data } = await axios.get('/api/lakewoodscoop/scrape');
            console.log(data)
            setPosts(data);
        };
        getPosts();
    }, []);
    return (
        <div className='container'>

            <div className='row'>
                <table className='table table-hover table-bordered table-striped'>
                    <thead>
                        <tr>
                            <th style={{width: "20%"}}>Image</th>
                            <th>Title</th>
                            <th>Blurb Of Text</th>
                            <th>Comments count</th>
                        </tr>
                    </thead>
                    <tbody>
                        {posts && posts.map(p =>
                            <tr>
                                <td>
                                    <img src={p.imageUrl} style={{width: 200}}/>
                                </td>
                                <td>
                                    <a target="_blank" href={p.linkUrl}>
                                        {p.title}    
                                    </a>
                                </td>
                                <td>
                                    {p.blurbOfText}
                                </td>
                                <td>{p.amountOfComments}</td>
                            </tr>
                            )}
                    </tbody>
                </table>
            </div>

        </div>
    )
}
export default Home;
